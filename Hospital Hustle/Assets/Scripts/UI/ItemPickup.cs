using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    void OnMouseDown()
    {
        InventoryUI inventoryUI = GameObject.Find("InventoryDisplay").GetComponent<InventoryUI>();
        Sprite itemSprite = GetComponent<SpriteRenderer>().sprite; // Get the sprite of the item GameObject
        inventoryUI.UpdateInventoryUI(itemSprite);
    }
}
