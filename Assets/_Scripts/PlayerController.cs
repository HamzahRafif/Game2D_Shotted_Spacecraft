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
            // Cek ke AmmoManager, apakah pelurunya masih ada?
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
}