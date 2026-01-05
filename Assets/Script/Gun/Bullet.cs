using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 3;
    public LayerMask whatIsSolid;

    private void Start()
    {
        if (BulletStatsManager.Instance != null)
        {
            speed = BulletStatsManager.Instance.speed;
            damage = BulletStatsManager.Instance.damage;
        }

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, moveDistance + 0.1f, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector2.up * moveDistance);
        }
    }
}