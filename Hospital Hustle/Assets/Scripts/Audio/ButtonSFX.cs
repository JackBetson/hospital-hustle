using UnityEngine;
using UnityEngine.UI;

public class ButtonAudioPlayer : MonoBehaviour
{
    public Button button;
    public AudioClip audioClip;

    void Start()
    {
        // Add a listener to the button's onClick event
        button.onClick.AddListener(PlayAudio);
    }

    void PlayAudio()
    {
        // Play the audio clip when the button is pressed
        GetComponent<AudioSource>().PlayOneShot(audioClip);
    }
}
