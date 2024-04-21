using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPatient : MonoBehaviour
{
    public string[] dialogueLines;
    public float dialogueSpeed = 0.05f;
    public float endlineWait = 1f;

    public bool Healed { get; private set; } = false;

    private DialogueDisplay dialogueDisplay;
    private TextMeshProUGUI interactionText;
    private bool isPlayerInTrigger = false;

    private void Start()
    {
        FindInteractionText();
        dialogueDisplay = FindObjectOfType<DialogueDisplay>(); // Assuming there's only one DialogueDisplay in the scene
        if (dialogueDisplay == null)
        {
            Debug.LogError("DialogueDisplay component not found in the scene.");
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && !Healed)
        {
            HandlePatientInteraction();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            FindInteractionText(); // Ensure the text component is ready

            if (interactionText != null)
            {
                if (Healed)
                {
                    interactionText.text = "Patient has been healed";
                }
                else
                {
                    interactionText.text = "Press E to administer";
                }
                interactionText.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if (interactionText != null)
            {
                interactionText.enabled = false;
            }
        }
    }

    private void FindInteractionText()
    {
        if (interactionText == null)
        {
            interactionText = GameObject.FindGameObjectWithTag("InteractionText")?.GetComponent<TextMeshProUGUI>();
            if (interactionText == null)
            {
                Debug.LogError("Interaction text component not found.");
            }
        }
    }

    private void HandlePatientInteraction()
    {
        if (InventoryManager.Instance.Medicine != null)
        {
            AdministerMedicine(InventoryManager.Instance.Medicine);
        }
    }

    public void AdministerMedicine(MedicineData medicine)
    {
        HealPatient();
        StartDialogue();
    }

    private void HealPatient()
    {
        Healed = true;
        FindInteractionText();

        if (interactionText != null)
        {
            interactionText.text = "Patient has been healed";
            interactionText.enabled = true;
        }
    }

    private void StartDialogue()
    {
        dialogueDisplay.DisplayLines(dialogueLines, dialogueSpeed, endlineWait);
    }
}
