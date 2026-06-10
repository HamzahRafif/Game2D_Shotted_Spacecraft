using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    [Header("Boss Stats")]
    public int maxHP = 100;
    public int scoreReward = 1000;

    [Header("UI")]
    public Slider hpBar;

    private int currentHP;
    private bool isDead;

    private void Start()
    {
        currentHP = maxHP;

        if (hpBar != null)
        {
            hpBar.maxValue = maxHP;
            hpBar.value = maxHP;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHP -= damage;

        if (hpBar != null)
            hpBar.value = currentHP;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.score += scoreReward;
            GameManager.Instance.BossDefeated();
        }

        Destroy(gameObject);
    }
}