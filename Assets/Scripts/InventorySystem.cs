using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;

    //public Sprite icon;
    public Transform itemContent;
    public GameObject inventoryItem;
    public GameObject inventoryUI;

    bool inventoryActive = false;

    public List<InventoryItemData> inventory = new List<InventoryItemData>();
    public List<GameObject> gos = new List<GameObject>();

    public static InventorySystem instance;


    public void Awake()
    {

        //if (instance != null && instance != this)
        //{
        //    Destroy(this.gameObject);

        //}
        //else
        //{
            instance = this;
        //}


        //inventory = new List<InventoryItemData>();
        //itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

    }
    public void Update()
    {
        //ListItems();
        //ToggleInventory();
        //CreateItemSlot();
        //InitializeInventory();
        //DisplayInventory();
    }




    public void Add(InventoryItemData item)
    {
        inventory.Add(item);
        //CreateItemSlot(item);
        //InitializeInventory();
        //ListItems();
    }

    public void Remove(InventoryItemData item)
    {
        inventory.Remove(item);
    }

    public void InitializeInventory()
    {
        for (int i = 0; i < gos.Count; i++)
        {
            //gos[i].transform.Find()
        }
        //for (int i = 0; i < inventory.Count; i++)
        //{
        //    CreateItemSlot(inventory[i]);
        //}
        foreach (Transform it in itemContent)
        {
            Destroy(it.gameObject);
        }
        foreach (var item in inventory)
        {
            CreateItemSlot(item);
        }
    }


    public void CreateItemSlot(InventoryItemData item)
    {

        GameObject obj = Instantiate(inventoryItem, itemContent);
        
        GameObject itemIcon = GameObject.Find("ItemIcon");
        GameObject itemName = GameObject.Find("ItemName");
        itemName.GetComponent<TextMeshProUGUI>().text = item.itemName;
        itemIcon.GetComponent<Image>().sprite = item.icon;
        //Debug.Log("Creating slot for" + item.itemName);
        gos.Add(obj);

    }

    
}
