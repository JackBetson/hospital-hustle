using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public Medicine Medicine { get; private set; }

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
        Medicine = medicine;
    }

    public void ClearMedicine()
    {
        Medicine = null;
    }
}
