using UnityEngine;
using static UnityEditor.Progress;

public class MedicineData
{
    public bool isDefibrillator;
    public string mainColor;
    public string subColor;
    public string icon;
    
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public MedicineData Medicine { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioClip pickupSFX;
    public void SetMedicine(Medicine medicine)
    {
        Medicine = new MedicineData
        {
            isDefibrillator = medicine.isDefibrillator,
            mainColor = medicine.mainColor,
            subColor = medicine.subColor,
            icon = medicine.icon

        };
        GetComponent<AudioSource>().PlayOneShot(pickupSFX);
    }

    public void ClearMedicine()
    {
        Medicine = null;
    }
}
