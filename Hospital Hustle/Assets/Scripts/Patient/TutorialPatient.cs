using TMPro;
using UnityEngine;

public class TutorialPatient : MonoBehaviour
{
    public bool Healed { get; private set; } = false;

    private TextMeshProUGUI _interactionText;
    private TextMeshProUGUI _notificationText;
    private bool _isPlayerInTrigger = false;

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
                else
                {
                    _interactionText.text = "Press E to administer";
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
        HealPatient();
    }

    private void HealPatient()
    {
        Healed = true;

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
    }

    private void DecreaseSuspicion()
    {
        GameManager.Instance.DecreaseSuspicion(1);
    }

    private void IncreaseSuspicion()
    {
        GameManager.Instance.IncreaseSuspicion(1);
    }

}
