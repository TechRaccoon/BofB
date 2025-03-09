using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialog;
    private bool OnDialogue = false; 
    

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialog);

    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("running");
        //Check if the player is in interaction distance
        if (other.tag == "Player")
        {
            Debug.Log("ON THE INTERACT ZONE");
            //show UI interaction icon 

            //interactDistance = true;
            if (Input.GetKeyDown(KeyCode.Space) && !OnDialogue) {

                Debug.Log(" !!!!!!!!!! starting dialogue");
                TriggerDialogue();
                OnDialogue = true;
                
            }
             
        }
    }

    //public void Interact() {
    //    //DialogueManager.Instance.StartDialogue();
    //}

    //public void EndInteraction() {
    //    //DialogueManager.Instance.EndDialogue();
    //}
}
