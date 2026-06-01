using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    [Header("Pool Settings")]
    public GameObject bulletPrefab; // Tarik prefab peluru ke sini nanti
    public int amountToPool = 20;   // Jumlah peluru yang disiapkan di awal

    public List<GameObject> pooledObjects;

    void Awake()
    {
        // Membuat ini jadi Singleton agar bisa dipanggil darimana saja
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        // Memproduksi peluru dan menyembunyikannya
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(bulletPrefab);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Fungsi untuk meminta peluru yang sedang nganggur
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}