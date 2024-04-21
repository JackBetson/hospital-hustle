using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int maxHealth = 9;
    public int currentHealth = 9;

    public Image[] healthUnits; // Array of Image components representing each unit of health

    void Start()
    {
        UpdateHealthBar();
    }

    // Update the visual representation of the health bar
    void UpdateHealthBar()
    {
        for (int i = 0; i < healthUnits.Length; i++)
        {
            if (i < currentHealth)
            {
                // Enable the image if the unit is active
                healthUnits[i].enabled = true;
            }
            else
            {
                // Disable the image if the unit is inactive
                healthUnits[i].enabled = false;
            }
        }
    }

    // Function to decrease health
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
        UpdateHealthBar();
    }

    // Function to increase health
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
        UpdateHealthBar();
    }
}
