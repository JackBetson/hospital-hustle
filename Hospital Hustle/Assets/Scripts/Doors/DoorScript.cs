using UnityEngine;
using TMPro;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private bool _isRightDoor;
    [SerializeField] private bool _isHallwayDoor;
    private const string CORRECT_PR_NAME = "CorrectPatientRoom";
    private const string INCORRECT_PR_NAME = "InCorrectPatientRoom";
    private const string HALLWAY_NAME = "MainLevel";

    private TextMeshProUGUI _interactionText;
    private SceneTransition _sceneTransition;
    private bool _isPlayerInTrigger = false;

    private void Start()
    {
        FindInteractionText();
        FindSceneTransition();
        if (_interactionText != null)
            _interactionText.enabled = false;
    }

    private void Update()
    {
        if (_isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            HandleDoorInteraction();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.GetCurrentTargetRoomId() == gameObject.name) Debug.Log("Correct room");
            _isPlayerInTrigger = true;
            FindInteractionText(); // Ensure the text component is ready
            FindSceneTransition(); // Ensure the SceneTransition component is ready

            if (_interactionText != null)
            {
                _interactionText.text = "Press E to enter";
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

    private void FindSceneTransition()
    {
        if (_sceneTransition == null)
        {
            _sceneTransition = FindObjectOfType<SceneTransition>();
            if (_sceneTransition == null)
            {
                Debug.LogError("SceneTransition component not found.");
            }
        }
    }

    private void HandleDoorInteraction()
    {
        if (_sceneTransition == null)
        {
            Debug.LogError("SceneTransition component is missing.");
            return;
        }

        DoorManager.enteredFromRightDoor = _isRightDoor;
        if (_isHallwayDoor)
        {
            Vector2 spawnPosition = CalculateSpawnPosition();
            DoorManager.SetLastDoorEnteredPosition(spawnPosition);
            LoadAppropriateScene();
        }
        else
        {
            _sceneTransition.FadeToScene(HALLWAY_NAME);
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        return (Vector2)transform.position + new Vector2(_isRightDoor ? -0.5f : 0.5f, 0);
    }

    private void LoadAppropriateScene()
    {
        if (GameManager.Instance.GetCurrentTargetRoomId() == gameObject.name)
        {
            _sceneTransition.FadeToScene(CORRECT_PR_NAME);
        }
        else
        {
            _sceneTransition.FadeToScene(INCORRECT_PR_NAME);
        }
    }
}
