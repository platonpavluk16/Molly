using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<InventoryItem> items = new List<InventoryItem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InventorySaveManager.LoadInventory();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string id, int amount = 1)
    {
        InventoryItem item = items.Find(i => i.id == id);
        if (item != null)
            item.count += amount;
        else
            items.Add(new InventoryItem { id = id, count = amount });

        InventorySaveManager.SaveInventory();
        InventoryUI.Instance?.Refresh();
    }

    public bool RemoveItem(string id, int amount = 1)
    {
        InventoryItem item = items.Find(i => i.id == id);
        if (item == null || item.count < amount)
            return false;

        item.count -= amount;
        if (item.count <= 0)
            items.Remove(item);

        InventorySaveManager.SaveInventory();
        InventoryUI.Instance?.Refresh();
        return true;
    }

    public void DropItem(string id, Vector3 position, GameObject prefab)
    {
        if (RemoveItem(id))
        {
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
