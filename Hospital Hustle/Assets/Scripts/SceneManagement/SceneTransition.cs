using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

    [SerializeField]
    private Animator _animator;
    private string _sceneToLoad;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.transform.root.gameObject); // Make the entire Canvas persistent
        }
        else if (Instance != this)
        {
            Destroy(this.transform.root.gameObject); // Destroy this if there's already an instance
        }
    }

    public void FadeToScene(string sceneName)
    {
        _sceneToLoad = sceneName;
        _animator.SetTrigger("startFadeOut");
    }

    public void OnFadeComplete()
    {
        StartCoroutine(LoadSceneAndFadeIn(_sceneToLoad));
    }

    private IEnumerator LoadSceneAndFadeIn(string sceneName)
    {
        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        // Wait until the new scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Ensure the animator is correctly targeted after new scene is loaded
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator component not found after scene load!");
                yield break; // Exit if no animator is found
            }
        }

        // Trigger fade-in after scene is loaded
        _animator.SetTrigger("startFadeIn");
    }
}
