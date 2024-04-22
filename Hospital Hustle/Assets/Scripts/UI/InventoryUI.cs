using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image itemImage;
    public Sprite defaultSprite; // Sprite to display when no item is selected

    void Start()
    {
        // Set default sprite when the UI initializes
        itemImage.sprite = defaultSprite;
    }

    public void UpdateInventoryUI(Sprite itemSprite)
    {
        if (itemSprite != null)
        {
            // Update image with the item's sprite
            itemImage.sprite = itemSprite;
        }
        else
        {
            // If item sprite is null, display default sprite
            itemImage.sprite = defaultSprite;
        }
    }

    // Add a method to clear the inventory UI
    public void ClearInventoryUI()
    {
        // Reset the item image to the default sprite
        itemImage.sprite = defaultSprite;
    }
}
