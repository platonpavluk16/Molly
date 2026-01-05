using System.Collections.Generic;
using UnityEngine;

public class InfiniteMapUltra : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableElement
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float chance;
    }

    public Transform player;
    public int chunkSize = 10;
    public int viewDistance = 4;
    public GameObject floorPrefab;
    public List<SpawnableElement> elements;

    private Dictionary<Vector2Int, GameObject> activeChunks = new();
    private Dictionary<string, Queue<GameObject>> pools = new();

    private Vector2Int lastPlayerChunk = new(99999, 99999);
    private int worldSeed;

    // 🔹 ЗАВАНТАЖУЄМО SEED
    void Start()
    {
        worldSeed = PlayerPrefs.GetInt("WORLD_SEED", 0);

        if (worldSeed == 0)
        {
            Debug.LogError("WORLD_SEED not found!");
        }
    }

    void Update()
    {
        Vector2Int currentPlayerChunk = new(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.y / chunkSize)
        );

        if (currentPlayerChunk != lastPlayerChunk)
        {
            lastPlayerChunk = currentPlayerChunk;
            RefreshChunks(currentPlayerChunk);
        }
    }

    void RefreshChunks(Vector2Int playerChunk)
    {
        List<Vector2Int> toRemove = new();

        foreach (var chunk in activeChunks)
        {
            if (Vector2Int.Distance(chunk.Key, playerChunk) > viewDistance + 1)
                toRemove.Add(chunk.Key);
        }

        foreach (var coord in toRemove)
            DespawnChunk(coord);

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int coord = new(playerChunk.x + x, playerChunk.y + y);
                if (!activeChunks.ContainsKey(coord))
                    SpawnChunk(coord);
            }
        }
    }

    void SpawnChunk(Vector2Int coord)
    {
        GameObject chunkObj = GetFromPool("Chunk", null, transform);
        chunkObj.transform.position = new Vector3(
            coord.x * chunkSize,
            coord.y * chunkSize,
            0
        );

        activeChunks.Add(coord, chunkObj);


        Random.InitState(
            worldSeed ^
            (coord.x * 73856093) ^
            (coord.y * 19349663)
        );

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Vector3 pos = chunkObj.transform.position + new Vector3(x, y, 0);

                GetFromPool(floorPrefab.name, floorPrefab, chunkObj.transform)
                    .transform.position = pos;

                SpawnRandomElement(pos, chunkObj.transform);
            }
        }
    }

    void SpawnRandomElement(Vector3 pos, Transform parent)
    {
        float roll = Random.value;
        float cumulative = 0f;

        foreach (var element in elements)
        {
            cumulative += element.chance;
            if (roll <= cumulative)
            {
                GetFromPool(element.prefab.name, element.prefab, parent)
                    .transform.position = pos;
                return;
            }
        }
    }

    void DespawnChunk(Vector2Int coord)
    {
        GameObject chunk = activeChunks[coord];

        List<GameObject> children = new();
        foreach (Transform child in chunk.transform)
            children.Add(child.gameObject);

        foreach (var obj in children)
            ReturnToPool(obj.name, obj);

        ReturnToPool("Chunk", chunk);
        activeChunks.Remove(coord);
    }

    GameObject GetFromPool(string key, GameObject prefab, Transform parent)
    {
        if (!pools.ContainsKey(key))
            pools[key] = new Queue<GameObject>();

        if (pools[key].Count > 0)
        {
            GameObject obj = pools[key].Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(parent);
            return obj;
        }

        GameObject newObj = prefab != null
            ? Instantiate(prefab, parent)
            : new GameObject(key);

        newObj.name = key;
        return newObj;
    }

    void ReturnToPool(string key, GameObject obj)
    {
        obj.SetActive(false);

        if (!pools.ContainsKey(key))
            pools[key] = new Queue<GameObject>();

        pools[key].Enqueue(obj);
    }
}
