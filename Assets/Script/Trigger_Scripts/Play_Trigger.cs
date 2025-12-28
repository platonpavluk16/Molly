using UnityEngine;

public class Play_Trigger : MonoBehaviour
{

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger area.");
        }
    }
}
