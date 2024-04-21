using UnityEngine;

public class ViewSupply : MonoBehaviour
{
    private Vector3 originalScale;
    private InventoryManager _inventoryManager;

    void Start()
    {
        // Store the original scale of the object
        originalScale = transform.localScale;
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void OnMouseOver()
    {
        // Enlarge the object by 2
        transform.localScale = originalScale * 1.45f;
    }

    void OnMouseExit()
    {
        // Restore the original scale of the object
        transform.localScale = originalScale;
    }

    private void OnMouseDown()
    {
        if(_inventoryManager != null)
            _inventoryManager.SetMedicine(gameObject.GetComponent<Medicine>());
    }
}