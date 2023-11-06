using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

    public InventoryItemData item;


    public void OnMouseDown()
    {
        if (GameObject.FindObjectOfType<Tooltip>().canInteract)
        {
            InventorySystem.instance.Add(item);

            TooltipManager._instance.RemoveTooltip();

            Destroy(gameObject);
            InventorySystem.instance.CreateItemSlot(item);

            
        }

    }
}
