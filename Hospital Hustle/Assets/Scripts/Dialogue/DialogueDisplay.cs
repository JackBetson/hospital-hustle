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

    public bool IsDialogueFinished { get { return currentLineIndex >= currentLines.Length; } }

    private void Start()
    {
        dialogueCanvas.SetActive(false); // Hide the canvas when the game starts
    }

    public void DisplayLines(string[] lines, float speed, float waitTime)
    {
        currentLines = lines;
        currentLineIndex = 0;
        dialogueSpeed = speed;
        endlineWait = waitTime;
        dialogueCanvas.SetActive(true); // Show the canvas
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        for (int i = 0; i < currentLines.Length; i++)
        {
            string line = currentLines[i];
            dialogueText.text = ""; // Clear the text before typing new line
            foreach (char c in line)
            {
                dialogueText.text += c; // Append each character to the text
                yield return new WaitForSeconds(dialogueSpeed);
            }
            if (i < currentLines.Length - 1)
            {
                yield return new WaitForSeconds(endlineWait);
                yield return WaitForLeftMouseButton(); // Wait for left mouse button
            }
        }
        dialogueCanvas.SetActive(false); // Hide the canvas when dialogue is finished
    }

    private IEnumerator WaitForLeftMouseButton()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null; // Wait until the left mouse button is pressed
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
