using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DoorTransition : MonoBehaviour
{
    public GameObject currentArea;
    public GameObject targetArea;
    public Image fadeImage;
    public float fadeDuration = 1f;
    private bool isPlayerNear = false;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Transition());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player entered door trigger");
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player exited door trigger");
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    private IEnumerator Transition()
    {
        yield return StartCoroutine(FadeTo(1)); // Fade to black
        currentArea.SetActive(false);
        targetArea.SetActive(true);
        yield return StartCoroutine(FadeTo(0)); // Fade back to game
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float alpha = fadeImage.color.a;

        for (float t = 0f; t < 1; t += Time.deltaTime / fadeDuration)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, targetAlpha, t));
            fadeImage.color = newColor;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
