using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Move_Main_Player_Shoot : MonoBehaviour
{
    public float speed = 5f;
    public int maxHp = 100;
    public int hp;

    void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (Keyboard.current.dKey.isPressed)
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (Keyboard.current.wKey.isPressed)
            transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Keyboard.current.sKey.isPressed)
            transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(40);
        }
    }

    void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            SceneManager.LoadScene("Main_Map");
        }
    }
}
