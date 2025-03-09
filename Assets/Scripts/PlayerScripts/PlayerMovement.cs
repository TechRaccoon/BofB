using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] NavMeshAgent agent;
    [SerializeField] CharacterController charCtrl;
    [SerializeField] PlayerAnim anim;

    //SFX
    //[SerializeField] AudioSource playerAudio;
    //[SerializeField] AudioClip shot;


    //private PlayerState state;

    //singular enemy clicked by player
    //public Vector2 targetDirection;
    //private GameObject targetObject;

    //movement variables
    private float horizInput;
    private float vertInput;
    private float speed = 3.0f;
    private float gravity = -9.8f;

    //direction for animations 
    private Vector2 direction;

    //coroutine running to avoid multiple calls
    //private bool isDamage = false;
    //private bool isCoroutineRunning = false;

    private void OnEnable()
    {
        // Subscribe to dialogue events
        //DialogueManager.Instance.OnDialogueStarted += DisableMovement;
        //DialogueManager.Instance.OnDialogueEnded += EnableMovement;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        //DialogueManager.Instance.OnDialogueStarted -= DisableMovement;
        //DialogueManager.Instance.OnDialogueEnded -= EnableMovement;
    }

    // Update is called once per frame
    void Update()
    {

        //movement handle
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizInput, gravity, vertInput) * speed * Time.deltaTime;
        charCtrl.Move(transform.TransformDirection(movement));

        //Animation directions call
        direction = new Vector2(horizInput, vertInput).normalized;

        // check if player is moving
        Vector2 inputVector = new Vector2(horizInput, vertInput);
        bool isMoving = inputVector.magnitude > 0.1f;

        //call to set the right animation sprite
        anim.SetDirection(direction, isMoving);
       
    }

    public void DisableMovement(object sender, EventArgs e) {
        this.charCtrl.enabled = false;
    }

    public void EnableMovement(object sender, EventArgs e) {
        this.charCtrl.enabled = true;
    }

    //IEnumerator DoubleAction() //single animation 2 shots , damage dealt to enemy
    //{
    //    if (targetObject != null)
    //    {
    //        isCoroutineRunning = true;
    //        targetObject.GetComponent<EnemyAI>().Health -= bulletDamage;
    //        playerAudio.PlayOneShot(shot);
    //        // Wait for the second shot.
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //    if (targetObject != null)
    //    {
    //        targetObject.GetComponent<EnemyAI>().Health -= bulletDamage;
    //        playerAudio.PlayOneShot(shot);

    //        // Wait for the next attack animation.
    //        yield return new WaitForSeconds(0.2325f);
    //    }
    //    isCoroutineRunning = false;
    //}

    //private void OnHealthChanged(int oldHealth)
    //{
    //    //Debug.Log("ONHEALTHCHANGED CALLED");
    //    if (health <= 0)
    //    {
    //        Messenger.Broadcast("PLAYER_DEAD");
    //    }
    //    else if (health > oldHealth)
    //    { //Healing
    //        Messenger.Broadcast("PLAYER_HEAL");
    //    }
    //    else if (health < oldHealth)
    //    { //taking damage
    //        isDamage = true;
    //    }
    //    else
    //    {
    //        //implement healing;

    //    }
    //}

    //IEnumerator DamageCoroutine(Vector2 direction)
    //{
    //    isCoroutineRunning = true;
    //    Messenger.Broadcast(GameEvent.PLAYER_DAMAGE);
    //    anim.SetDirection(direction);
    //    yield return new WaitForSeconds(0.15f);
    //    isCoroutineRunning = false;
    //    isDamage = false;
    //}

    //public void ClearTarget()
    //{
    //    Debug.Log("clearing target");
    //    this.targetObject = null;
    //}
    //public void SetTarget(GameObject obj)
    //{
    //    Debug.Log("SetTarget(): " + obj.gameObject.tag);
    //    this.targetObject = obj;
    //}
}