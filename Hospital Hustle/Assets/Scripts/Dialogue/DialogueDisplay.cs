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
    private bool skipText;

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
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        dialogueText.text = ""; // Clear existing text
        for (int i = 0; i < currentLines[currentLineIndex].Length; i++)
        {
            if (skipText && Input.GetMouseButtonDown(0))
            {
                dialogueText.text = currentLines[currentLineIndex]; // Show full text if left mouse button is pressed
                skipText = false; // Reset skipText flag
                break; // Exit the loop
            }
            else
            {
                dialogueText.text += currentLines[currentLineIndex][i];
                yield return new WaitForSeconds(dialogueSpeed);
            }
        }
        if (waitForInput)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Wait for left mouse button click
        }
        else
        {
            yield return new WaitForSeconds(endlineWait);
        }
        NextLine();
    }

    public void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < currentLines.Length)
        {
            StartCoroutine(AnimateText());
        }
        else
        {
            dialogueCanvas.SetActive(false); // Hide the canvas when dialogue is finished
        }
    }

    private void Update()
    {
        if (!waitForInput && Input.GetMouseButtonDown(0))
        {
            skipText = true; // Set skipText flag to true when left mouse button is pressed
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
