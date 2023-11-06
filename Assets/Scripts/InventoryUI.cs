using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    InventorySystem inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = InventorySystem.instance; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        Debug.Log("Update");
    }
}
