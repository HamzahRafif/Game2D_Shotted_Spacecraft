using System.Collections.Generic;
using UnityEngine;

public class ExplosionPooler : MonoBehaviour
{
    public static ExplosionPooler Instance;

    [Header("Pool Settings")]
    public GameObject explosionPrefab; // Tempat menaruh prefab ExplosionEffect
    public int amountToPool = 10;      // Menyiapkan 10 peluru ledakan cadangan di awal

    private List<GameObject> pooledExplosions;

    void Awake()
    {
        // Membuat sistem Singleton agar mudah dipanggil dari script lain
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Membuat daftar list untuk menyimpan objek ledakan
        pooledExplosions = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(explosionPrefab);
            obj.SetActive(false); // Sembunyikan semua ledakan di awal game
            pooledExplosions.Add(obj);
        }
    }

    // Fungsi khusus untuk membangunkan ledakan di posisi musuh yang hancur
    public void SpawnExplosion(Vector3 position)
    {
        for (int i = 0; i < pooledExplosions.Count; i++)
        {
            // Cari objek ledakan di gudang yang sedang menganggur (tidak aktif)
            if (!pooledExplosions[i].activeInHierarchy)
            {
                pooledExplosions[i].transform.position = position; // Pindahkan posisi ke tempat musuh meledak
                pooledExplosions[i].SetActive(true);               // Hidupkan animasinya
                return;
            }
        }
    }
}