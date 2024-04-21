using UnityEngine;
using static UnityEditor.Progress;

public class MedicineData
{
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

    public void SetMedicine(Medicine medicine)
    {
        Debug.Log("Medicine set to inventory");
        Medicine = new MedicineData
        {
            mainColor = medicine.mainColor,
            subColor = medicine.subColor,
            icon = medicine.icon
        };
        Debug.Log(Medicine.mainColor);
        Debug.Log(Medicine.subColor);
        Debug.Log(Medicine.icon);
    }

    public void ClearMedicine()
    {
        Medicine = null;
    }
}
