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
    private bool _isRightDoor;
    [SerializeField]
    private bool _isHallwayDoor;

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
            DoorManager.enteredFromRightDoor = _isRightDoor;
            if (_isHallwayDoor)
            {
                Vector2 spawnPosition = (Vector2)transform.position + (new Vector2(_isRightDoor ? -0.5f : 0.5f, 0));
                DoorManager.SetLastDoorEnteredPosition(spawnPosition);
            }
                
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
