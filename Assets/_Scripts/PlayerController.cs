using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Screen Boundaries")]
    public float minX = -2.5f;
    public float maxX = 2.5f;
    public float minY = -4.5f;
    public float maxY = 0f;

    [Header("Shooting Settings")]
    public Transform firePoint;
    public float fireRate = 0.2f;

    [Header("Invincibility Frames (Pengaman Bug)")]
    public float invincibilityDuration = 1f; // Kebal 1 detik setelah kena hit
    private bool isInvincible = false;
    private float nextFireTime = 0f;

    void Update()
    {
        MovePlayer();
        Shoot();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 newPosition = transform.position + new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            if (AmmoManager.Instance != null && AmmoManager.Instance.TryShoot())
            {
                nextFireTime = Time.time + fireRate;

                GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();

                if (bullet != null)
                {
                    bullet.transform.position = firePoint.position;
                    bullet.transform.rotation = firePoint.rotation;
                    bullet.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    // BARIS DETEKTIF: Menampilkan pesan di Console setiap kali ada yang menyentuh Player
    Debug.LogWarning("ALERT! Player disentuh oleh objek: " + other.name + " | Tag Objek: " + other.tag);

    if (isInvincible) return;

    // PERBAIKAN: Pesawat HANYA menerima damage dari "Enemy" dan "EnemyBullet"
    // Peluru pesawatmu sendiri (yang punya Tag "Bullet") akan diabaikan sepenuhnya!
    if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TakeDamage();
            StartCoroutine(BecomeInvincibleCoroutine());
        }
    }
}

    // Coroutine untuk mengaktifkan jeda kebal temporer (mencegah instant game over)
    private System.Collections.Generic.IEnumerator<WaitForSeconds> BecomeInvincibleCoroutine()
    {
        isInvincible = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        
        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            if (sr != null) sr.enabled = !sr.enabled; // Membuat efek pesawat berkedip
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }
        
        if (sr != null) sr.enabled = true; // Pastikan sprite menyala kembali
        isInvincible = false;
    }
}