using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private SceneTransition _sceneTransition;
    [SerializeField]
    private string _sceneToLoad;
    [SerializeField]
    private TextMeshProUGUI _interactionText;
    [SerializeField]
    private bool isRightDoor;

    private bool _isPlayerInTrigger = false;

    private void Start()
    {
        if (_interactionText != null)
            _interactionText.enabled = false;
    }

    private void Update()
    {
        // Check if the player is in the trigger area and if they press the 'E' key
        if (_isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            _sceneTransition.FadeToScene(_sceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to a player
        if (other.CompareTag("Player"))
        {
            _interactionText.text = "Press E to enter";
            _interactionText.enabled = true;
            _isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Disable the text and reset the trigger flag when the player exits
        if (other.CompareTag("Player"))
        {
            _interactionText.enabled = false;
            _isPlayerInTrigger = false;
        }
    }
}
