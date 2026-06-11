using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifeTime = 3f; // Peluru akan hilang dalam 3 detik
    public int damage = 1;      // Besar damage peluru

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

    // 🔥 PERBAIKAN AMAN: Hanya merespon musuh, abaikan Player sepenuhnya!
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika tidak sengaja menyentuh Player sendiri saat spawn, abaikan!
        if (other.CompareTag("Player")) return;

        // Jika menabrak pembatas layar
        if (other.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
            return;
        }

        // Jika menabrak Boss / Musuh biasa
        if (other.CompareTag("Enemy"))
        {
            BossEnemy boss = other.GetComponent<BossEnemy>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            gameObject.SetActive(false); // Sembunyikan peluru kembali ke pooler
        }
    }
}