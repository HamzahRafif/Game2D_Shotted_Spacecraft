using System.Collections;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [Header("Bullet Setup")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Mode Timing")]
    public float modeDuration = 30f;

    [Header("Spread Settings")]
    public int spreadCount = 5;
    public float spreadAngle = 30f;
    public float spreadFireRate = 0.5f;

    [Header("Spiral Settings")]
    public float spiralSpeed = 200f;
    public float spiralFireRate = 0.1f;

    private float modeTimer;
    private bool isSpreadMode = true;

    private float shootTimer;

    void Start()
    {
        modeTimer = modeDuration;
    }

    void Update()
    {
        HandleModeSwitch();

        if (isSpreadMode)
        {
            SpreadShoot();
        }
        else
        {
            SpiralShoot();
        }
    }

    // 🔥 SWITCH MODE EVERY 30 SECONDS
    void HandleModeSwitch()
    {
        modeTimer -= Time.deltaTime;

        if (modeTimer <= 0)
        {
            isSpreadMode = !isSpreadMode;
            modeTimer = modeDuration;

            Debug.Log("Boss Mode: " +
                (isSpreadMode ? "SPREAD" : "SPIRAL"));
        }
    }

    // 🔥 SPREAD SHOT
    void SpreadShoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer < spreadFireRate)
            return;

        shootTimer = 0f;

        float step = spreadAngle / (spreadCount - 1);

        for (int i = 0; i < spreadCount; i++)
        {
            float angle =
                -spreadAngle / 2 +
                step * i;

            Quaternion rot =
                Quaternion.Euler(0, 0, angle);

            Instantiate(
                bulletPrefab,
                firePoint.position,
                rot
            );
        }
    }

    // 🔥 SPIRAL SHOT
    void SpiralShoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer < spiralFireRate)
            return;

        shootTimer = 0f;

        float angle = Time.time * spiralSpeed;

        Quaternion rot =
            Quaternion.Euler(0, 0, angle);

        Instantiate(
            bulletPrefab,
            firePoint.position,
            rot
        );
    }
}