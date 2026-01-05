using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public float spawnRate = 2f;    
    public bool stopSpawning = true; 
    public float spawnRadius = 5f;  

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime && !stopSpawning)
        {
            Spawn();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void Spawn()
    {
        if (objectToSpawn == null) return;

        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}