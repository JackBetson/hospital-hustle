using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCabinetExit : MonoBehaviour
{
    private SceneTransition _sceneTransition;
    private const string HALLWAY_NAME = "Tutorial";
    void Start()
    {
        FindSceneTransition();
    }

    private void OnMouseEnter()
    {
        FindSceneTransition();
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

    public void ExitToMainScene()
    {
        if (_sceneTransition == null)
        {
            FindSceneTransition();
            if (_sceneTransition != null)
            {
                _sceneTransition.FadeToScene(HALLWAY_NAME);
            }
            else
            {
                Debug.LogError("SceneTransition component not found.");
            }
        }
        else
        {
            _sceneTransition.FadeToScene(HALLWAY_NAME);
        }
    }
}