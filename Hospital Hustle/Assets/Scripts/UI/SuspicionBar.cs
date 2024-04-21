using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionBar : MonoBehaviour
{
    public int maxSuspicion = 9;
    public int currentSuspicion = 0;

    public Image[] suspicionUnits; // Array of Image components representing each unit of suspicion

    void Start()
    {
        UpdateSuspicionBar();
    }

    // Update the visual representation of the suspicion bar
    void UpdateSuspicionBar()
    {
        for (int i = 0; i < suspicionUnits.Length; i++)
        {
            if (i < currentSuspicion)
            {
                // Enable the image if the unit is active
                suspicionUnits[i].enabled = true;
            }
            else
            {
                // Disable the image if the unit is inactive
                suspicionUnits[i].enabled = false;
            }
        }
    }

    // Function to increase suspicion
    public void IncreaseSuspicion(int suspicionAmount)
    {
        currentSuspicion += suspicionAmount;
        currentSuspicion = Mathf.Clamp(currentSuspicion, 0, maxSuspicion); // Ensure suspicion doesn't go below 0 or above maxSuspicion
        UpdateSuspicionBar();
    }

    // Function to decrease suspicion
    public void DecreaseSuspicion(int suspicionAmount)
    {
        currentSuspicion -= suspicionAmount;
        currentSuspicion = Mathf.Clamp(currentSuspicion, 0, maxSuspicion); // Ensure suspicion doesn't go below 0 or above maxSuspicion
        UpdateSuspicionBar();
    }
}
