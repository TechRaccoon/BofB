using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<string> sentences;

    private GameObject player;

    private void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartDialogue(Dialogue dialogue) {

        //Print the name of the respective NPC
        nameText.text = dialogue.name;

        //Disable player movement while dialog is happening
        player.GetComponent<PlayerMovement>().enabled = false;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {

            sentences.Enqueue(sentence);
        }

        DisplayNexteSentence();
    }

    public void DisplayNexteSentence() {

        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopCoroutine(TypeSentence(sentence));
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue() {
        Debug.Log("End of dialogue");

        //Make the UI dissapear

        //Return movement to the player
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
