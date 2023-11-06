
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string itemName;
    public Sprite icon;
    //public GameObject prefab;
}
