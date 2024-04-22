using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseDialogue : MonoBehaviour
{
    public string[] lines;
    public float dialogueSpeed = 0.05f;
    public float endlineWait = 1f;

    public AudioClip typingSound; // Audio clip for typing sound
    private AudioSource audioSource;

    private DialogueDisplay dialogueDisplay;

    private void Start()
    {
        dialogueDisplay = FindObjectOfType<DialogueDisplay>(); // Assuming there's only one DialogueDisplay in the scene
        if (dialogueDisplay == null)
        {
            Debug.LogError("DialogueDisplay component not found in the scene.");
        }
        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartDialogue(); // Start dialogue without parameters
            GameManager.Instance.IncreaseSuspicion(1);
        }
    }

    private void StartDialogue()
    {
        dialogueDisplay.DisplayLines(lines, dialogueSpeed, endlineWait);

        StartCoroutine(TypeDialogue()); // Call TypeDialogue without parameters
    }

    private IEnumerator TypeDialogue()
    {
        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                // Play typing sound for each character
                audioSource.PlayOneShot(typingSound);
                yield return new WaitForSeconds(dialogueSpeed);
            }
            yield return new WaitForSeconds(endlineWait);
            // Wait for left mouse button to be pressed
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }
        }
    }
}
