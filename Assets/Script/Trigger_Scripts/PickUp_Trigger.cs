using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(itemName);
            Destroy(gameObject);
        }
    }
}
