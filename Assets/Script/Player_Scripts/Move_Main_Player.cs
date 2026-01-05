using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Move_Main_Player : MonoBehaviour
{

    public int hp = 100;

    void Update()
    {
        if(Keyboard.current.wKey.isPressed)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 5);
        }
        if(Keyboard.current.sKey.isPressed)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
        if(Keyboard.current.aKey.isPressed)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 5);
        }
        if(Keyboard.current.dKey.isPressed)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 5);
        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0 )
        {
            SceneManager.LoadScene("Main_Map");
        }
    }

    
}
