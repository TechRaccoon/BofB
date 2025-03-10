using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialog;
    private bool OnDialogue = false;
    private Coroutine inputCoroutine;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && inputCoroutine == null) {
            inputCoroutine = StartCoroutine(CheckForInput());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && inputCoroutine != null)
        {
            StopCoroutine(CheckForInput());
        }
    }

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialog);
    }

    public void NextDialogue()
    {
        FindAnyObjectByType<DialogueManager>().DisplayNexteSentence();
    }

    private IEnumerator CheckForInput() {
        while (true) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!OnDialogue)
                {
                    TriggerDialogue();
                    OnDialogue = true;
                }
                else
                {
                    NextDialogue();
                }
            }
            yield return null;
        }

    }

    //public void Interact() {
    //    //DialogueManager.Instance.StartDialogue();
    //}

    //public void EndInteraction() {
    //    //DialogueManager.Instance.EndDialogue();
    //}
}
