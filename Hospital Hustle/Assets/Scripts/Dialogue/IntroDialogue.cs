using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroDialogue : MonoBehaviour
{
    public string[] lines;
    public float dialogueSpeed = 0.05f;
    public float endlineWait = 1f;
    public string sceneToLoad; // Name of the scene to load after dialogue finishes
    private DialogueDisplay dialogueDisplay;

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

}
