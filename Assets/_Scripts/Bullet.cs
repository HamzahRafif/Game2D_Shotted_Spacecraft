using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifeTime = 3f; // Peluru akan hilang dalam 3 detik
    public int damage = 1;      // Besar damage peluru (buat musuh nanti)

    private float deactivateTime;

    void OnEnable()
    {
        // Menentukan kapan peluru harus disembunyikan setiap kali aktif
        deactivateTime = Time.time + lifeTime;
    }

    void Update()
    {
        // Menggerakkan peluru lurus ke atas
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Menyembunyikan peluru jika sudah melewati batas waktu
        if (Time.time >= deactivateTime)
        {
            gameObject.SetActive(false);
        }
    }
}