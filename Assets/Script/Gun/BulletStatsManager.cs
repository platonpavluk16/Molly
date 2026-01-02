using UnityEngine;

public class BulletStatsManager : MonoBehaviour
{
    public static BulletStatsManager Instance;

    public int damage = 3;
    public float speed = 10f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Load()
    {
        damage = PlayerPrefs.GetInt("BulletDamage", damage);
        speed = PlayerPrefs.GetFloat("BulletSpeed", speed);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("BulletDamage", damage);
        PlayerPrefs.SetFloat("BulletSpeed", speed);
        PlayerPrefs.Save();
    }
}
