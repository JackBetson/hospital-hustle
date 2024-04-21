using UnityEngine;
using TMPro;

public class CabinetScript : MonoBehaviour
{
    private const string BLUE_CABINET_NAME = "BlueCabinet";
    private const string ORANGE_CABINET_NAME = "OrangeCabinet";
    private const string PINK_CABINET_NAME = "PinkCabinet";
    private const string RED_CABINET_NAME = "RedCabinet";

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
            HandleCabinetInteraction();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = true;
            FindInteractionText(); // Ensure the text component is ready
            FindSceneTransition(); // Ensure the SceneTransition component is ready

            if (_interactionText != null)
            {
                _interactionText.text = "Press E to open";
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

    private void HandleCabinetInteraction()
    {
        if (_sceneTransition == null)
        {
            Debug.LogError("SceneTransition component is missing.");
            return;
        }

        LoadAppropriateScene();
    }

    private void LoadAppropriateScene()
    {
        string cabinetName = gameObject.name;
        switch (cabinetName)
        {
            case BLUE_CABINET_NAME:
                _sceneTransition.FadeToScene(BLUE_CABINET_NAME);
                break;
            case ORANGE_CABINET_NAME:
                _sceneTransition.FadeToScene(ORANGE_CABINET_NAME);
                break;
            case PINK_CABINET_NAME:
                _sceneTransition.FadeToScene(PINK_CABINET_NAME);
                break;
            case RED_CABINET_NAME:
                _sceneTransition.FadeToScene(RED_CABINET_NAME);
                break;
            default:
                Debug.LogError("Invalid cabinet name.");
                break;
        }
    }
}
