using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    private GameObject player;

    private void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartDialogue(Dialogue dialogue) {

        Debug.Log("...Starting Conversation with " + dialogue.name);

        player.GetComponent<PlayerMovement>().enabled = false;

        sentences.Clear();


        foreach (string sentence in dialogue.sentences) {

            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNexteSentence() {

        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
    }

    public void EndDialogue() {
        Debug.Log("End of dialogue");

        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
