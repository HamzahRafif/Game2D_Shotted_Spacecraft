using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public float moveSpeed;
    public int scoreReward;
}
