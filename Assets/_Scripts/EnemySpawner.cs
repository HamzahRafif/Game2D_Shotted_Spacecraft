using UnityEngine;

public class EnemySpawner : MonoBehaviour // Ganti nama class ini jika nama script-mu ItemSpawner
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;  // Cetakan musuh
    public float spawnInterval = 1.5f; // Jarak waktu antar gelombang

    [Header("Cluster Settings")]
    public int minEnemiesPerWave = 1; // Paling sedikit keluar 1
    public int maxEnemiesPerWave = 3; // Paling banyak keluar 3 sekaligus

    [Header("Position Boundaries")]
    public float minX = -2.5f; // Batas kiri layar (Sesuaikan dengan ukuran layarmu)
    public float maxX = 2.5f;  // Batas kanan layar (Sesuaikan dengan ukuran layarmu)

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        // Hitung mundur untuk spawn
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemyWave();
            timer = spawnInterval; // Reset timer
        }
    }

    void SpawnEnemyWave()
    {
        // Tentukan jumlah musuh yang mau dikeluarkan di gelombang ini secara acak
        int enemiesToSpawn = Random.Range(minEnemiesPerWave, maxEnemiesPerWave + 1);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Tentukan posisi X secara acak untuk masing-masing musuh
            float randomX = Random.Range(minX, maxX);

            // Tambahkan sedikit variasi tinggi (Y) agar kalau keluar 3, nggak sejajar kaku banget
            float randomYOffset = Random.Range(0f, 1f);

            Vector2 spawnPos = new Vector2(randomX, transform.position.y + randomYOffset);

            // Cetak musuh ke panggung
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}