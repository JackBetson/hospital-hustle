using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public Medicine medicine;

    void OnMouseDown()
    {
        // Assume InventoryUI is attached to a GameObject named "InventoryDisplay"
        InventoryUI inventoryUI = GameObject.Find("InventoryDisplay").GetComponent<InventoryUI>();
        InventoryManager.Instance.SetMedicine(medicine);
        inventoryUI.UpdateInventoryUI(InventoryManager.Instance.Medicine);
    }
}
