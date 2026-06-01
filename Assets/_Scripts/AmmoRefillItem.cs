using UnityEngine;

public class AmmoRefillItem : MonoBehaviour
{
    [Header("Item Settings")]
    public float fallSpeed = 2f; // Kecepatan turun item

    void Update()
    {
        // Item bergerak pelan ke bawah
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // Hancurkan item jika tidak diambil dan keluar dari layar bawah
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    // Mendeteksi jika ada objek yang menabrak item ini
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang menabrak adalah objek ber-tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Panggil fungsi isi ulang peluru di AmmoManager
            if (AmmoManager.Instance != null)
            {
                AmmoManager.Instance.RefillAmmo();
            }

            // Hancurkan item hijau ini setelah berhasil diambil
            Destroy(gameObject);
        }
    }
}