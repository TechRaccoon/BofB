using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { IDLE, PATROL, CHASE, ATTACK, DAMAGE, DEAD };

public class EnemyAI : MonoBehaviour
{

    //[SerializeField] EnemyAnim anim;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;

    //Enemy Variables
    [SerializeField] public int health = 50;
    [SerializeField] public float attackSpeed = 0.5f;  //frame dependant
    [SerializeField] public int enemyDamage;

    //Player variables
    [SerializeField] PlayerMovement player;
    [SerializeField] Transform target;

    //SFX
    //[SerializeField] AudioSource enemySound;
    //[SerializeField] AudioClip attackSound;
    //[SerializeField] AudioClip damageSound;
    //[SerializeField] AudioClip dieSound;
    //[SerializeField] AudioClip moveSound;

    private EnemyState state;
    public int Health
    {
        get => health;
        set { health = value; UpdateHealth(); }
    }

    //Range variables 
    private float distanceToTarget = float.MaxValue;    // distance to target - default to far away
    [SerializeField] float chaseRange = 3.5f;                     // when target is closer than this, chase!
    [SerializeField] float attackRange = 0.8f;                   //change based on sprite size
    private Vector2 direction;

    //Death Variables
    private float destructionTimer = 0;
    private float timeToWaitBeforeDestuction = 2.0f;

    //coroutine checker
    private bool isCoroutineRunning = false;
    private void Start()
    {
        state = EnemyState.IDLE;
    }

    void Update()
    {

        //Debug.Log("HEALTH: " + enemyController.Health + "STATE: " + state);

        distanceToTarget = Vector3.Distance(transform.position, target.position);

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        direction = new Vector2(directionToTarget.x, directionToTarget.z);
        //direction = new Vector2(agent.transform.forward.x, agent.transform.forward.z);

        //Debug.Log(direction);
        switch (state)
        {
            case EnemyState.IDLE: Update_Idle(); break;
            case EnemyState.CHASE: Update_Chase(); break;
            case EnemyState.ATTACK: Update_Attack(); break;
            case EnemyState.PATROL: Update_Patrol(); break;
            case EnemyState.DAMAGE: Update_Damage(); break;
            case EnemyState.DEAD: Update_Death(); break;
            default: break;
        }
    }


    void Update_Idle()
    {
        agent.isStopped = true;                 // stop the agent
        //anim.SetDirection(direction);           // change animations                             
        if (distanceToTarget <= chaseRange)
        {
            SetState(EnemyState.CHASE);
        }
    }

    void Update_Chase()
    {
        agent.isStopped = false;
        //anim.SetDirection(direction);
        agent.SetDestination(target.transform.position);  // follow the target
        if (!isCoroutineRunning)
        {
            StartCoroutine(MoveSound());
        }
        if (distanceToTarget > chaseRange)
        {
            SetState(EnemyState.IDLE);
        }
        else if (distanceToTarget <= attackRange)
        {
            SetState(EnemyState.ATTACK);
        }
    }

    void Update_Attack()
    {
        transform.LookAt(target);
        agent.velocity = Vector3.zero;
        //anim.SetDirection(direction);
        if (!isCoroutineRunning)
        {
            StartCoroutine(InflictDamage());
        }
        if (distanceToTarget > attackRange)
        {
            SetState(EnemyState.CHASE);
        }
    }

    void Update_Patrol()
    {
        //FUTURE IMPLEMENTATION
    }

    void Update_Damage()
    {
        if (!isCoroutineRunning)
        {
            //anim.SetDirection(direction);    //update the animation
            StartCoroutine(TakingDamage());
        }
    }

    public void Enter_Death()
    {
        agent.isStopped = true;
        //anim.SetDirection(direction);
        //enemySound.PlayOneShot(dieSound);
    }
    public void Update_Death()
    {
        destructionTimer += Time.deltaTime;
        if (destructionTimer > timeToWaitBeforeDestuction)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    /////////////// COROUTINES ////////////////

    IEnumerator TakingDamage() //DAMAGE COROUTINE
    {
        agent.isStopped = true;       //stop enemy
        isCoroutineRunning = true;    //coroutine checker

        yield return new WaitForSeconds(0.1f);
        //enemySound.PlayOneShot(damageSound);
        agent.isStopped = false;
        isCoroutineRunning = false;

        chaseRange *= 3;
        SetState(EnemyState.CHASE);
    }


    IEnumerator InflictDamage()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(attackSpeed); //wait until the last frame of animation 
        //enemySound.PlayOneShot(attackSound);
        if (distanceToTarget <= attackRange)
        {        //re-check if the player is still on attack range

            //player.Health -= enemyDamage;
        }
        isCoroutineRunning = false;
    }

    IEnumerator MoveSound()
    {
        isCoroutineRunning = true;
        //enemySound.PlayOneShot(moveSound);
        yield return //new WaitForSeconds(moveSound.length);
        isCoroutineRunning = false;
    }

    /////////////// END OF COROUTINES ////////////////


    //Health Listener
    private void UpdateHealth()
    {
        if (health <= 0)
        {
            SetState(EnemyState.DEAD);
            Enter_Death();

        }
        else
        {
            SetState(EnemyState.DAMAGE);
        }
    }


    // Getters and setters
    public EnemyState GetEnemyState()
    {
        return this.state;
    }
    public void SetState(EnemyState newState)
    {
        state = newState;
    }
    public Transform GetTarget()
    {
        return this.target;
    }
    public PlayerMovement GetPlayer()
    {
        return this.player;
    }

    public void RegisterAsTarget(Vector2 enemy)
    {
        //player.targetDirection = enemy;
        //player.SetTarget(this.gameObject);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);  // draw a circle to show chase range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);  // draw a circle to show attack range
    }

}