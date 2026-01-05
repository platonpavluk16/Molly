using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public GameObject inventoryPanel;
    public TextMeshProUGUI inventoryText;

    private bool isOpen = false;
    private List<string> itemKeys = new List<string>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);

            if (isOpen)
                UpdateUI();
        }
    }

    public void Refresh()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        inventoryText.text = "";
        itemKeys.Clear();

        if (Inventory.Instance == null || Inventory.Instance.items == null)
            return;

        int index = 0;
        foreach (var item in Inventory.Instance.items)
        {
            inventoryText.text += $"<link={index}>{item.id} x{item.count}</link>\n";
            itemKeys.Add(item.id);
            index++;
        }
    }

    public void OnTextClick(string linkID, string linkText, int linkIndex)
    {
        int index = int.Parse(linkID);
        if (index >= 0 && index < itemKeys.Count)
        {
            string itemToDrop = itemKeys[index];

            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{itemToDrop}");
            if (prefab != null)
            {
                Vector3 dropPos = Camera.main.transform.position + Camera.main.transform.forward * 2f;
                Inventory.Instance.DropItem(itemToDrop, dropPos, prefab);
                UpdateUI();
            }
            else
            {
                Debug.LogWarning($"Prefab for {itemToDrop} not found in Resources/Prefabs");
            }
        }
    }
}
