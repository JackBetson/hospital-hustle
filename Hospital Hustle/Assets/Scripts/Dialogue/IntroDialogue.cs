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
    public AudioClip typingSound; // Audio clip for typing sound
    private AudioSource audioSource;

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
        Debug.Log("Dialogue finished. Changing scene to: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator TypeDialogue()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            foreach (char c in line)
            {
                // Play typing sound for each character
                audioSource.PlayOneShot(typingSound);
                yield return new WaitForSeconds(dialogueSpeed);
            }
            if (i < lines.Length - 1)
            {
                yield return new WaitForSeconds(endlineWait);
            }
        }
    }
}
