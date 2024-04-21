using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerTutorial : MonoBehaviour
{
    public Transform playerSpawnPoint; // Reference to the player's spawn point in the new scene

    public void ChangeScene(string sceneName)
    {
        // Set the player's spawn position before loading the scene
        PlayerPrefs.SetFloat("PlayerSpawnX", playerSpawnPoint.position.x);
        PlayerPrefs.SetFloat("PlayerSpawnY", playerSpawnPoint.position.y);
        
        SceneManager.LoadScene(sceneName);
    }
}
