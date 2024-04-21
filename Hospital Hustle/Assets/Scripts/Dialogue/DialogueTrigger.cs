using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] currentLines;
    public float dialogueSpeed = 0.05f;
    public float endlineWait = 1f;

    private bool hasDisplayed = false;
    private bool isDisplaying = false;
    private DialogueDisplay dialogueDisplay;
    public AudioClip typingSound; // Audio clip for typing sound
    private AudioSource audioSource;

    private void Start()
    {
        dialogueDisplay = FindObjectOfType<DialogueDisplay>(); // Assuming there's only one DialogueDisplay in the scene
        if (dialogueDisplay == null)
        {
            Debug.LogError("DialogueDisplay component not found in the scene.");
        }
    }

    private IEnumerator TypeDialogue()
    {
        for (int i = 0; i < currentLines.Length; i++)
        {
            string line = currentLines[i];
            foreach (char c in line)
            {
                // Play typing sound for each character
                audioSource.PlayOneShot(typingSound);
                yield return new WaitForSeconds(dialogueSpeed);
            }
            if (i < currentLines.Length - 1)
            {
                yield return new WaitForSeconds(endlineWait);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDisplaying && !hasDisplayed)
        {
            isDisplaying = true;
            dialogueDisplay.DisplayLines(currentLines, dialogueSpeed, endlineWait);
            hasDisplayed = true; // Mark dialogue as displayed
        }
    }

    // Method to reset dialogue state when the dialogue finishes
    public void ResetDialogueState()
    {
        hasDisplayed = false;
        isDisplaying = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResetDialogueState(); // Reset dialogue state when player exits the trigger
        }
    }
}