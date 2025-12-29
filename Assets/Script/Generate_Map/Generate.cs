using System.Collections.Generic;
using UnityEngine;

public class InfiniteMapUltra : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableElement
    {
        public string name;
        public GameObject prefab;
        [Range(0f, 1f)] public float chance;
    }

    public Transform player;
    public int chunkSize = 10;
    public int viewDistance = 4;
    public GameObject floorPrefab;
    public List<SpawnableElement> elements;

    private Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    private Vector2Int lastPlayerChunk = new Vector2Int(-999, -999);

    void Update()
    {
        Vector2Int currentPlayerChunk = new Vector2Int(
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
        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var chunk in activeChunks)
        {
            if (Vector2Int.Distance(chunk.Key, playerChunk) > viewDistance + 1)
                toRemove.Add(chunk.Key);
        }

        foreach (var coord in toRemove)
        {
            DespawnChunk(coord);
        }

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int coord = new Vector2Int(playerChunk.x + x, playerChunk.y + y);
                if (!activeChunks.ContainsKey(coord)) SpawnChunk(coord);
            }
        }
    }

    void SpawnChunk(Vector2Int coord)
    {
        GameObject chunkObj = GetFromPool("Chunk", null, transform);
        chunkObj.name = $"Chunk_{coord.x}_{coord.y}";
        chunkObj.transform.position = new Vector3(coord.x * chunkSize, coord.y * chunkSize, 0);
        activeChunks.Add(coord, chunkObj);

        Random.InitState(coord.x * 31337 ^ coord.y * 13337);

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Vector3 pos = chunkObj.transform.position + new Vector3(x, y, 0);
                GetFromPool("Floor", floorPrefab, chunkObj.transform).transform.position = pos;

                foreach (var element in elements)
                {
                    if (Random.value < element.chance)
                    {
                        GetFromPool(element.name, element.prefab, chunkObj.transform).transform.position = pos;
                        break; 
                    }
                }
            }
        }
    }

    void DespawnChunk(Vector2Int coord)
    {
        GameObject chunk = activeChunks[coord];
        List<Transform> children = new List<Transform>();
        foreach (Transform child in chunk.transform) children.Add(child);
        
        foreach (Transform child in children)
        {
            string poolKey = child.name.Replace("(Clone)", "").Trim();
            ReturnToPool(poolKey, child.gameObject);
        }

        ReturnToPool("Chunk", chunk);
        activeChunks.Remove(coord);
    }

    GameObject GetFromPool(string key, GameObject prefab, Transform parent)
    {
        if (!pools.ContainsKey(key)) pools.Add(key, new Queue<GameObject>());

        if (pools[key].Count > 0)
        {
            GameObject obj = pools[key].Dequeue();
            obj.SetActive(true);
            obj.transform.parent = parent;
            return obj;
        }
        return Instantiate(prefab ?? new GameObject(key), parent);
    }

    void ReturnToPool(string key, GameObject obj)
    {
        obj.SetActive(false);
        if (!pools.ContainsKey(key)) pools.Add(key, new Queue<GameObject>());
        pools[key].Enqueue(obj);
    }
}