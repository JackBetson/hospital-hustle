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
            itemIcon.sprite = InventoryManager.Instance.Medicine.gameObject.GetComponent<SpriteRenderer>().sprite;
            itemIcon.enabled = true;
        }
        else
        {
            itemIcon.enabled = false;
        }
    }
}
