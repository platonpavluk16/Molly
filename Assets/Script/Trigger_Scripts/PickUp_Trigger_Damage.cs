using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUp_Trigger_Damage : MonoBehaviour
{
    public int addDamage = 2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (BulletStatsManager.Instance != null)
            {
                BulletStatsManager.Instance.damage += addDamage;
                BulletStatsManager.Instance.Save();
                SceneManager.LoadScene("Main_Map");
            }
            else
            {
                Debug.LogError("BulletStatsManager НЕ знайдений!");
            }

            Destroy(gameObject);
        }
    }
}
