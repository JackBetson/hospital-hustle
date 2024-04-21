using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCabinetExit : MonoBehaviour
{
    private TutorialSceneManager _sceneManager; // Use TutorialSceneManager
    private const string HALLWAY_NAME = "Tutorial";

    public Vector2 spawnPosition; // Store the spawn position

    public void ExitToMainScene()
    {
        if (_sceneManager == null)
        {
            FindSceneManager();
            if (_sceneManager != null)
            {
                // Pass spawn position to FadeToScene
                _sceneManager.FadeToScene(HALLWAY_NAME, spawnPosition);
            }
            else
            {
                Debug.LogError("TutorialSceneManager component not found.");
            }
        }
        else
        {
            // Pass spawn position to FadeToScene
            _sceneManager.FadeToScene(HALLWAY_NAME, spawnPosition);
        }
    }

    private void FindSceneManager()
    {
        if (_sceneManager == null)
        {
            _sceneManager = FindObjectOfType<TutorialSceneManager>();
            if (_sceneManager == null)
            {
                Debug.LogError("TutorialSceneManager component not found.");
            }
        }
    }
}
