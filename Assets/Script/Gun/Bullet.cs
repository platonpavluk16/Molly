using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    public float distance = 0.1f;
    public int damage = 3;

    public LayerMask whatIsSolid;

    private void Start()
    {
        speed = BulletStatsManager.Instance.speed;
        damage = BulletStatsManager.Instance.damage;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        RaycastHit2D hitInfo =
            Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

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
    }
}
