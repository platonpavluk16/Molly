using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage. Current health: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy destroyed.");
        }

    }


    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            float step = 2f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
    }
    

}
