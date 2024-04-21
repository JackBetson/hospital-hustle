using TMPro;
using UnityEngine;

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
                IncreaseSuspicion();
                break;
        }
        InventoryManager.Instance.ClearMedicine();
    }

    public AudioClip HealSFX;
    private void HealPatient()
    {

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
        GetComponent<AudioSource>().PlayOneShot(KillSFX);
        Debug.Log("Patient has died");
        GameManager.Instance.EndGame();
    }

    private void AdministerDefibrillator()
    {
        Debug.Log("Defibrillator administered");
        HealPatient();
        GameManager.Instance.EndGame();
    }
}
