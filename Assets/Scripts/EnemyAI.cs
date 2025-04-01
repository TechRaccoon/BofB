using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { IDLE, PATROL, CHASE, ATTACK, DAMAGE, DEAD };

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    public EnemyAnim anim;
    private EnemyState state;

    //Player variables
    [SerializeField] PlayerMovement player;
    [SerializeField] Transform target;

    // Battle event
    public static event System.Action OnBattleTransition;
    private bool _combatTriggered = false;

    //SFX
    //[SerializeField] AudioSource enemySound;
    //[SerializeField] AudioClip attackSound;
    //[SerializeField] AudioClip damageSound;
    //[SerializeField] AudioClip dieSound;
    //[SerializeField] AudioClip moveSound;

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
        anim = GetComponentInChildren<EnemyAnim>();
        state = EnemyState.IDLE;
    }

    void Update()
    {

        distanceToTarget = Vector3.Distance(transform.position, target.position);

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        direction = new Vector2(directionToTarget.x, directionToTarget.z);

        //Debug.Log(direction);
        switch (state)
        {
            case EnemyState.IDLE: Update_Idle(); break;
            case EnemyState.CHASE: Update_Chase(); break;
            //case EnemyState.ATTACK: Update_Attack(); break;
            //case EnemyState.PATROL: Update_Patrol(); break;
            //case EnemyState.DAMAGE: Update_Damage(); break;
            //case EnemyState.DEAD: Update_Death(); break;
            default: break;
        }
        //Debug.Log($"Raw Direction: {(target.position - transform.position).normalized}");
        //Debug.Log($"2D Direction: {direction}");
    }


    void Update_Idle()
    {
        agent.isStopped = true;                 // stop the agent
        anim.SetDirection(direction);           // change animations                             
        if (distanceToTarget <= chaseRange)
        {
            SetState(EnemyState.CHASE);
        }
    }

    void Update_Chase()
    {
        agent.isStopped = false;
        anim.SetDirection(direction);
        agent.SetDestination(target.transform.position);  // follow the target
        if (!isCoroutineRunning)
        {
            StartCoroutine(MoveSound());
        }
        if (distanceToTarget > chaseRange)
        {
            SetState(EnemyState.IDLE);
        }
        else if (distanceToTarget <= attackRange && !_combatTriggered)
        {
            Update_BattleTrigger();
            Debug.Log("COMBAT SCENE TRIGGERED!");
            _combatTriggered = true;
        }
        

    }

    void Update_BattleTrigger()
    {
        OnBattleTransition?.Invoke();
    }


    void Update_Patrol()
    {
        //FUTURE IMPLEMENTATION
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

    IEnumerator MoveSound()
    {
        isCoroutineRunning = true;
        //enemySound.PlayOneShot(moveSound);
        yield return //new WaitForSeconds(moveSound.length);
        isCoroutineRunning = false;
    }

    /////////////// END OF COROUTINES ////////////////



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
        player.targetDirection = enemy;
        player.SetTarget(this.gameObject);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);  // draw a circle to show chase range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);  // draw a circle to show attack range
    }

}