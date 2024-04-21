using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartDialogue : MonoBehaviour
{
    public string[] lines;
    public float dialogueSpeed = 0.05f;
    public float endlineWait = 1f;
    public string sceneToLoad; // Name of the scene to load after dialogue finishes
    public Image dialogueImage; // Reference to the Image component displaying the image
    private DialogueDisplay dialogueDisplay;
    private int currentLineIndex = 0;

    private void Start()
    {
        dialogueDisplay = FindObjectOfType<DialogueDisplay>(); // Assuming there's only one DialogueDisplay in the scene
        if (dialogueDisplay == null)
        {
            Debug.LogError("DialogueDisplay component not found in the scene.");
        }
        else
        {
            dialogueDisplay.dialogueCanvas.SetActive(true); // Activate the dialogue canvas
            dialogueDisplay.DisplayLines(lines, dialogueSpeed, endlineWait);
            StartCoroutine(WaitForDialogueFinish());
        }
    }

    IEnumerator WaitForDialogueFinish()
    {
        yield return new WaitUntil(() => dialogueDisplay.IsDialogueFinished);
        // Change scene after dialogue finishes
        SceneManager.LoadScene(sceneToLoad);
    }

    void Update()
    {
        // Check if the current line being displayed is line 5
        if (currentLineIndex == 4) // Arrays are zero-indexed
        {
            dialogueImage.enabled = true; // Show the image
        }
        else
        {
            dialogueImage.enabled = false; // Hide the image
        }
    }

    // Call this method to advance the dialogue to the next line
    public void NextLine()
    {
        currentLineIndex++;
    }
}
