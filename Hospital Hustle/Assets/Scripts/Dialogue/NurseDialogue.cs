using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseDialogue : MonoBehaviour
{
    public string[] lines;
    public float dialogueSpeed = 0.05f;
    public float endlineWait = 1f;

    private DialogueDisplay dialogueDisplay;

    private void Start()
    {
        dialogueDisplay = FindObjectOfType<DialogueDisplay>(); // Assuming there's only one DialogueDisplay in the scene
        if (dialogueDisplay == null)
        {
            Debug.LogError("DialogueDisplay component not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueDisplay.DisplayLines(lines, dialogueSpeed, endlineWait);
        }
    }
}
