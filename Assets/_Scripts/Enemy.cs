using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float fallSpeed = 3f;
    public int scoreValue = 10;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
            if (ExplosionPooler.Instance != null)
            {
                ExplosionPooler.Instance.SpawnExplosion(transform.position);
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddKillAndScore(scoreValue);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            if (ExplosionPooler.Instance != null)
            {
                ExplosionPooler.Instance.SpawnExplosion(transform.position);
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}