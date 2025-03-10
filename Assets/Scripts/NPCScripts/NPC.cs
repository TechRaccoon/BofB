using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialog;
    public bool onDialogue = false;
    private Coroutine inputCoroutine;
    private Canvas npcCanvas;

    private void Start()
    {
        // Get the canvas component from children (including inactive)
        npcCanvas = gameObject.GetComponentInChildren<Canvas>();
        Debug.Log("the value of canvas is" + npcCanvas);
        if (npcCanvas != null) npcCanvas.enabled = false;
    }

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
            StopCoroutine(inputCoroutine);
            inputCoroutine = null;
        }
    }

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialog , npcCanvas);
    }

    public void NextDialogue()
    {
        FindAnyObjectByType<DialogueManager>().DisplayNextSentence();
    }

    private IEnumerator CheckForInput()
    {
        while (true) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!onDialogue)
                {
                    TriggerDialogue();
                    onDialogue = true;
                }
                else
                {
                    NextDialogue();
                    if (!npcCanvas.enabled) {
                        onDialogue = false;
                    }
                }
            }
            yield return null;
        }

    }

}
