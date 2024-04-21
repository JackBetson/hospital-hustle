using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialCabinet : MonoBehaviour
{
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
        Vector2 spawnPosition = CalculateSpawnPosition();
        LoadAppropriateScene();
    }

    private Vector2 CalculateSpawnPosition()
    {
        return (Vector2)transform.position;
    }


    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void LoadAppropriateScene()
    {
        string sceneName = "Tutorial";
        ChangeScene(sceneName);
    }
}
