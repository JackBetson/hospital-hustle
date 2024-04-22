using TMPro;
using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour
{
    public static Patient Instance { get; private set; }
    // Patient fields for required medicine properties
    [SerializeField] private string requiredColour;
    [SerializeField] private string requiredSubColour;
    [SerializeField] private string requiredIcon;

    public bool Healed { get; private set; } = false;

    private TextMeshProUGUI _interactionText;
    private TextMeshProUGUI _notificationText;
    private bool _isPlayerInTrigger = false;

    public string[] healedDialogue;
    public string[] damagedDialogue;
    public string[] killedDialogue;

    private DialogueDisplay dialogueDisplay;
    public float dialogueSpeed = 0.05f;
    public float endlineWait = 0f;

    public AudioClip typingSound; // Audio clip for typing sound
    private AudioSource audioSource;

    private Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindInteractionText();
        FindNotificationText();
        dialogueDisplay = FindObjectOfType<DialogueDisplay>(); // Assuming there's only one DialogueDisplay in the scene
        if (dialogueDisplay == null)
        {
            Debug.LogError("DialogueDisplay component not found in the scene.");
        }

        // Initialize audio source
        audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on patient GameObject.");
        }

    }

    private void Update()
    {
        if (_isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            HandlePatientInteraction();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = true;
            FindInteractionText(); // Ensure the text component is ready

            if (_interactionText != null)
            {
                if (Healed)
                {
                    _interactionText.text = "Patient has been healed";
                }
                else if (InventoryManager.Instance.Medicine != null)
                {
                    _interactionText.text = "Press E to administer";
                }
                else
                {
                    _interactionText.text = "Patient requires medicine";
                }
                _interactionText.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = false;
            if (_interactionText != null)
            {
                _interactionText.enabled = false;
            }
        }
    }

    private void FindInteractionText()
    {
        if (_interactionText == null)
        {
            _interactionText = GameObject.FindGameObjectWithTag("InteractionText")?.GetComponent<TextMeshProUGUI>();
            if (_interactionText == null)
            {
                Debug.LogError("Interaction text component not found.");
            }
        }
    }

    private void FindNotificationText()
    {
        if (_notificationText == null)
        {
            _notificationText = GameObject.FindGameObjectWithTag("NotificationText")?.GetComponent<TextMeshProUGUI>();
            if (_notificationText == null)
            {
                Debug.LogError("Notification text component not found.");
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
        int matchingTraits = 0;

        if(GameManager.Instance.IsDefibRound)
        {
            if (medicine.isDefibrillator)
            {
                AdministerDefibrillator();
                return;
            }
            else
            {
                IncreaseSuspicion();
                FindNotificationText();
                if(_notificationText != null)
                {
                    _notificationText.text = "They're having a heart attack idiot!";
                    _notificationText.enabled = true;
                }
                return;
            }
        }

        // Check each trait for a match
        if (medicine.mainColor == requiredColour) matchingTraits++;
        if (medicine.subColor == requiredSubColour) matchingTraits++;
        if (medicine.icon == requiredIcon) matchingTraits++;
        
        // Determine outcome based on the number of matching traits
        switch (matchingTraits)
        {
            case 3: // All traits match
                HealPatient();
                break;
            case 0: // No traits match
                KillPatient();
                break;
            default: // Some but not all traits match
                DamagePatient();
                break;
        }

        InventoryManager.Instance.ClearMedicine();
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.ClearInventoryUI();
        }
    }

    private void StartDialogue(string[] dialogueToUse)
    {
        dialogueDisplay.DisplayLines(dialogueToUse, dialogueSpeed, endlineWait);

        StartCoroutine(TypeDialogue(dialogueToUse));
    }

    private IEnumerator TypeDialogue(string[] dialogueToUse)
    {
        foreach (string line in dialogueToUse)
        {
            foreach (char c in line)
            {
                // Play typing sound for each character
                audioSource.PlayOneShot(typingSound);
                yield return new WaitForSeconds(dialogueSpeed);
            }
            yield return new WaitForSeconds(endlineWait);
        }
    }

    public AudioClip HealSFX;
    private void HealPatient()
    {
        string[] dialogueToUse = healedDialogue;
        Healed = true;

        if (Healed) GetComponent<AudioSource>().PlayOneShot(HealSFX);

        FindNotificationText();
        FindInteractionText();

        if (_interactionText != null)
        {
            _interactionText.text = "Patient has been healed";
            _interactionText.enabled = true;
        }

        if (_notificationText != null)
        {
            _notificationText.text = "Return to the hall ASAP!";
            _notificationText.enabled = true;
        }

        DecreaseSuspicion();
        GameManager.Instance.StopHealthDecay();
        StartDialogue(dialogueToUse);

        if (animator != null)
        {
            animator.SetTrigger("Healed");
        }

    }

    private void DamagePatient()
    {
        string[] dialogueToUse = damagedDialogue;
        Healed = false;

        FindNotificationText();
        FindInteractionText();

        if (_interactionText != null)
        {
            _interactionText.text = "Wrong item!!";
            _interactionText.enabled = true;
        }

        IncreaseSuspicion();
        IncreaseSuspicion();
        StartDialogue(dialogueToUse);

        if (animator != null)
        {
            animator.SetTrigger("Healed");
        }
    }

    private void DecreaseSuspicion()
    {
        GameManager.Instance.DecreaseSuspicion(1);
    }

    private void IncreaseSuspicion()
    {
        GameManager.Instance.IncreaseSuspicion(1);
    }

    public AudioClip KillSFX;

    private void KillPatient()
    {
        string[] dialogueToUse = killedDialogue;
        GetComponent<AudioSource>().PlayOneShot(KillSFX);
        Debug.Log("Patient has died");

        if (animator != null)
        {
            animator.SetTrigger("Healed");
        }

        StartCoroutine(DialogueAndEndGame(dialogueToUse));
    }

    private IEnumerator DialogueAndEndGame(string[] dialogueToUse)
    {
        // Start dialogue
        StartDialogue(dialogueToUse);

        // Wait for dialogue to finish
        yield return new WaitForSeconds(dialogueToUse.Length * (dialogueSpeed + endlineWait));

        // End game
        GameManager.Instance.EndGame();
    }

    private void AdministerDefibrillator()
    {
        Debug.Log("Defibrillator administered");
        HealPatient();
        GameManager.Instance.EndGame();
    }
}
