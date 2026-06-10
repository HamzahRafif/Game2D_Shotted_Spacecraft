using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 1;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage();
        }

        Destroy(gameObject);
    }
}