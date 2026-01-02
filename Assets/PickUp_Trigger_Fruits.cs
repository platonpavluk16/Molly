using UnityEngine;

public class PickUp_Trigger_Fruits : MonoBehaviour
{
    public int fruitHealthRestore = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Move_Main_Player player = collision.GetComponent<Move_Main_Player>();
            if (player != null)
            {
                player.HP += fruitHealthRestore;
                Debug.Log("Fruit picked up! Restored " + fruitHealthRestore + " HP.");
                Destroy(gameObject);
            }
            if (player.HP > 100)
            {
                player.HP = 100;
            }
        }
    }
}
