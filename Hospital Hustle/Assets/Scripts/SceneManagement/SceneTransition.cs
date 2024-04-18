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
        DontDestroyOnLoad(this.transform.root.gameObject); // This will make the entire Canvas persistent
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
        _animator = GetComponent<Animator>();  // Re-grab the Animator in case it's lost reference

        // Trigger fade-in after scene is loaded
        _animator.SetTrigger("startFadeIn");
    }
}
