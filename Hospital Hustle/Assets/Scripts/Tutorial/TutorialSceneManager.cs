using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour
{
    public float transitionTime = 1f;

    private string nextSceneName;
    public Vector2 spawnPosition; // Added to store the spawn position

    public void FadeToScene(string sceneName)
    {
        FadeToScene(sceneName, Vector2.zero); // Call the overloaded method with default spawn position
    }

    // Overloaded method to accept spawn position
    public void FadeToScene(string sceneName, Vector2 spawnPos)
    {
        nextSceneName = sceneName;
        spawnPosition = spawnPos; // Store the spawn position
    }

    // This method is called by the animation event
    public void OnTransitionComplete()
    {
        SceneManager.LoadScene(nextSceneName); // Load the next scene
    }

    // Get the spawn position for the player in the next scene
    public Vector2 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
