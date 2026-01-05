using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class InventorySaveManager
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "inventory.json");

    public static void DeleteInventoryFile()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Old inventory file deleted successfully.");
        }
    }

    public static void SaveInventory()
    {
        if (Inventory.Instance == null)
        {
            Debug.LogWarning("Cannot save: Inventory instance is null");
            return;
        }

        string json = JsonUtility.ToJson(new InventoryData(Inventory.Instance.items), true);
        File.WriteAllText(filePath, json);
        Debug.Log("Inventory saved to: " + filePath);
    }

    public static void LoadInventory()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("No inventory save file found. Starting fresh.");
            return;
        }

        if (Inventory.Instance == null)
        {
            Debug.LogWarning("Cannot load: Inventory instance not found in scene.");
            return;
        }

        try 
        {
            string json = File.ReadAllText(filePath);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);

            Inventory.Instance.items.Clear();
            if (data.items != null)
            {
                Inventory.Instance.items.AddRange(data.items);
            }

            InventoryUI.Instance?.Refresh();
            Debug.Log("Inventory loaded successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load inventory: " + e.Message);
        }
    }

    [System.Serializable]
    private class InventoryData
    {
        public List<InventoryItem> items;
        public InventoryData(List<InventoryItem> items)
        {
            this.items = items;
        }
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnSceneLoaded()
    {

        if (Inventory.Instance != null)
        {
            LoadInventory();
        }
    }
}