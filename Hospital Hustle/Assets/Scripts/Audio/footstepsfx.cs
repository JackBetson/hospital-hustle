using UnityEngine;

public class KeyAudioPlayer : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Set the audio clip to loop
        audioSource.loop = true;
    }

    void Update()
    {
        // Check if any of the specified keys are pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // If the audio source is not already playing, start playing the audio clip
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
        else
        {
            // If none of the keys are pressed, stop playing the audio clip
            audioSource.Stop();
        }
    }
}
