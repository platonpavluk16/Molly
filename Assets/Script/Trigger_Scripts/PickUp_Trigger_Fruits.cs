using UnityEngine;

public class PickUpTriggerFruits : MonoBehaviour
{
    public int fruitHealthRestore = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Move_Main_Player player = collision.GetComponent<Move_Main_Player>();
        if (player == null) return;

        player.hp += fruitHealthRestore;

        if (player.hp > 100)
        {
            player.hp = 100;
        }

        Debug.Log("Fruit picked up! Restored " + fruitHealthRestore + " HP.");

        Destroy(gameObject);
    }
}
