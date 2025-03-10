using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<string> sentences;

    private GameObject player;

    private Canvas npcCanvas;

    private bool isTyping = false;
    private string currentSentence;

    private void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartDialogue(Dialogue dialogue, Canvas canvas) {

        // Store reference to the NPC's canvas
        npcCanvas = canvas;

        //Enable canvas UI
        if (npcCanvas != null)
        {
            Debug.Log("canvas is not null");
            npcCanvas.enabled = true;
        }
         

        //Print the name to the canvas of the respective NPC
        nameText.text = dialogue.name;

        //Disable player movement while dialog is happening
        player.GetComponentInChildren<PlayerAnim>().SetDirection(new Vector2(0, 0), false);
        player.GetComponent<PlayerMovement>().enabled = false;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {

            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // If we're typing, complete the current sentence
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        // If no more sentences, end dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Get next sentence and start typing
        currentSentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(currentSentence));
    }

    public void EndDialogue() {
        Debug.Log("End of dialogue");

        // Disable the canvas
        if (npcCanvas != null)
        {
            npcCanvas.enabled = false;
        }
        //Return movement to the player
        player.GetComponent<PlayerMovement>().enabled = true;

        // Clear sentences *only when dialogue fully ends*
        sentences.Clear();
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f); // Typing speed
        }

        isTyping = false; // Now it's okay to press Space to continue
    }

    public void AssignNPC(NPC npc)
    {
        nameText = npc.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(t => t.gameObject.name == "Name");
        dialogueText = npc.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(t => t.gameObject.name == "Dialogue");
    }
}
