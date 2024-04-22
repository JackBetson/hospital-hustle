using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefibClueGiver : MonoBehaviour
{
    public GameObject imageToShow; // Reference to the image object you want to appear
    public Button buttonToShow; // Reference to the button object you want to appear
    public float activationDistance = 2f; // Distance at which the player can activate the image and button
    public AudioClip ClueSFX;

    private bool playerInRange = false;
    private GameManager gameManager;

    private void Start()
    {
        // Find the GameManager object and get its GameManager component
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Check if the player is in range and pressing the left mouse button
        if (playerInRange && Input.GetMouseButtonDown(0))
        {
            // Check if the mouse cursor is pointing at the object
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure the z-coordinate is 0 for 2D
            Vector2 objectPosition = transform.position;

            if (Vector2.Distance(mousePosition, objectPosition) < activationDistance)
            {
                // Activate the image and button when the player clicks while pointing at the object
                {
                    imageToShow.SetActive(true);
                    buttonToShow.gameObject.SetActive(true);
                    GetComponent<AudioSource>().PlayOneShot(ClueSFX);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the object entering the trigger is the player
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the object exiting the trigger is the player
        {
            playerInRange = false;
            imageToShow.SetActive(false); // Deactivate the image when the player exits the trigger area
            buttonToShow.gameObject.SetActive(false); // Deactivate the button when the player exits the trigger area
        }
    }
}
