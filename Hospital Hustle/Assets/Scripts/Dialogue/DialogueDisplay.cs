using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Text dialogueText;
    private string[] currentLines;
    private int currentLineIndex;
    private float dialogueSpeed;
    private float endlineWait;
    private bool waitForInput;
    private string fullText;

    public bool IsDialogueFinished { get { return currentLineIndex >= currentLines.Length; } }

    private void Start()
    {
        dialogueCanvas.SetActive(false); // Hide the canvas when the game starts
    }

    public void DisplayLines(string[] lines, float speed, float waitTime, bool waitForInput = true)
    {
        currentLines = lines;
        currentLineIndex = 0;
        dialogueSpeed = speed;
        endlineWait = waitTime;
        this.waitForInput = waitForInput;
        dialogueCanvas.SetActive(true); // Show the canvas
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        for (int i = 0; i < currentLines.Length; i++)
        {
            string line = currentLines[i];
            foreach (char c in line)
            {
                yield return new WaitForSeconds(dialogueSpeed);
            }
            if (i < currentLines.Length - 1)
            {
                yield return new WaitForSeconds(endlineWait);
            }
        }
    }

    public void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < currentLines.Length)
        {
            StartCoroutine(TypeDialogue());
        }
        else
        {
            dialogueCanvas.SetActive(false); // Hide the canvas when dialogue is finished
        }
    }

    public string GetLineAtIndex(int index)
    {
        if (index >= 0 && index < currentLines.Length)
        {
            return currentLines[index];
        }
        else
        {
            Debug.LogWarning("Index out of range.");
            return null;
        }
    }
}
