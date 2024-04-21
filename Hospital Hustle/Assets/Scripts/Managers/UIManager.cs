using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Image itemIcon; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateInventoryUI()
    {
        if (InventoryManager.Instance.Medicine != null)
        {
            //Will need to figure out another way to get the sprite, this could be called straight from ViewSupply to use the gameobject instead of the Medicine script
            //itemIcon.sprite = InventoryManager.Instance.Medicine.gameObject.GetComponent<SpriteRenderer>().sprite;
            itemIcon.enabled = true;
        }
        else
        {
            itemIcon.enabled = false;
        }
    }
}
