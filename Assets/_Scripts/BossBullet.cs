using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 1;

    private void Update()
    {
        // Tetap menggunakan gerakan bawaan awal temanmu (lurus ke bawah secara lokal)
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pengaman: Jangan hancur jika menyentuh Collider si Boss sendiri atau Enemy lain
        if (other.CompareTag("Enemy") || other.name.Contains("Boss"))
        {
            return;
        }

        // Peluru hanya meledak dan memberi damage jika mengenai Player
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage();
            }
            
            Destroy(gameObject); // Hancur secara sah setelah kena Player
        }
    }
}