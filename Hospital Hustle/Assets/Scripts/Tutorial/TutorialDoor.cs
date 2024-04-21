using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TutorialDoor : MonoBehaviour
{
    private TextMeshProUGUI _interactionText;
    private SceneTransition _sceneTransition;
    private bool _isPlayerInTrigger = false;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

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
            string sceneName = "MainLevel";
            ChangeScene(sceneName);
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
}
