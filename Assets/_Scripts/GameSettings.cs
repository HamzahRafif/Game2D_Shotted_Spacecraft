using UnityEngine;

[CreateAssetMenu(
    fileName = "NewGameSettings",
    menuName = "Game/Game Settings"
)]
public class GameSettings : ScriptableObject
{
    [Header("Stage Settings")]
    public float stageDuration = 300f;
    public int minKillsToPass = 40;

    [Header("Player Settings")]
    public int startingHealth = 3;

    [Header("Score Settings")]
    public int defaultEnemyScore = 100;

    [Header("Economy")]
    public int startingCredits = 0;

    [Header("Boss Settings")]
    public bool hasBoss = false;
    public GameObject bossPrefab;
    public int killsBeforeBoss = 50;

    [Header("Boss Intro")]
    public float warningDuration = 3f;
}