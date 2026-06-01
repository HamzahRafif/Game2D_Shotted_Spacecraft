using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject itemPrefab;      // Masukkan prefab item hijau di sini
    public float spawnInterval = 10f;  // Item muncul setiap 10 detik
    public float spawnYPosition = 6f;  // Ketinggian item muncul

    [Header("Spawn Area Width")]
    public float minX = -2.5f;
    public float maxX = 2.5f;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        // Jika sudah waktunya, jatuhkan item
        if (Time.time >= nextSpawnTime)
        {
            SpawnItem();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnItem()
    {
        if (itemPrefab != null)
        {
            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPosition = new Vector3(randomX, spawnYPosition, 0f);
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }
} 