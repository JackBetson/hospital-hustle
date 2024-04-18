using UnityEngine;
using System.Collections;

public class ViewSupply : MonoBehaviour
{
    void OnMouseOver()
    {
        // Enlarge the object by 2
        transform.localScale = new Vector3(1.5F, 1.5F, 0);
    }

    void OnMouseExit()
    {
        // Shrink the object by 2
        transform.localScale = new Vector3(1F, 1F, 0);
    }

}