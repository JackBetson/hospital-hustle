using UnityEngine;

public class ViewSupply : MonoBehaviour
{
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
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
        InventoryManager.Instance.SetMedicine(gameObject.GetComponent<Medicine>());
    }
}