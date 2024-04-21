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

    public void UpdateInventoryUI(MedicineData item)
    {
        if (item != null && !string.IsNullOrEmpty(item.icon))
        {
            // Load sprite based on item's icon path
            Sprite itemSprite = Resources.Load<Sprite>(item.icon);
            if (itemSprite != null)
            {
                // Update image with the item's sprite
                itemImage.sprite = itemSprite;
            }
            else
            {
                Debug.LogWarning("Sprite not found for item: " + item.icon);
            }
        }
        else
        {
            // If item is null or icon is empty, display default sprite
            itemImage.sprite = defaultSprite;
        }
    }
}
