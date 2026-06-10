using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Boss System")]
    public Transform bossSpawnPoint;
    public GameObject BossWarningPanel;

    [Header("Game Settings")]
    public GameSettings gameSettings;

    [Header("Stage Runtime")]
    private int minKillsToPass;
    private float timer;
    private bool stageCompleted = false;

    [Header("Boss Runtime")]
    private bool bossSpawned = false;
    private bool bossDefeated = false;

    [Header("Player Stats")]
    public int playerHealth = 3;
    public int score = 0;
    public int kills = 0;
    public int credits = 0;
    public bool isGameOver = false;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killText;

    [Header("Panels")]
    public GameObject gameOverPanel;
    public GameObject stageClearPanel;
    public TextMeshProUGUI gameOverReasonText;

    [Header("HP UI")]
    public Image[] heartIcons;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    [Header("Spawner")]
    public GameObject enemySpawner;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (gameSettings != null)
        {
            minKillsToPass = gameSettings.minKillsToPass;
            timer = gameSettings.stageDuration;
            playerHealth = gameSettings.startingHealth;
            credits = gameSettings.startingCredits;
        }
        else
        {
            minKillsToPass = 40;
            timer = 300f;
            playerHealth = 3;
        }

        gameOverPanel?.SetActive(false);
        stageClearPanel?.SetActive(false);
        BossWarningPanel?.SetActive(false);

        UpdateUI();
    }

    private void Update()
    {
        if (isGameOver || stageCompleted)
            return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0) timer = 0;

            UpdateUI();
        }
        else
        {
            CheckStageResult();
        }
    }

    // 🔥 KILL SYSTEM (FIXED)
    public void AddKillAndScore(int points)
    {
        if (isGameOver || stageCompleted)
            return;

        score += points;
        kills++;

        UpdateUI();

        // BOSS STAGE
        if (gameSettings != null && gameSettings.hasBoss)
        {
            if (!bossSpawned &&
                kills >= gameSettings.killsBeforeBoss)
            {
                StartCoroutine(SpawnBossRoutine());
            }
        }
        // NORMAL STAGE
        else
        {
            if (kills >= minKillsToPass)
            {
                TriggerStageClear();
            }
        }
    }

    public void TakeDamage()
    {
        if (isGameOver || stageCompleted)
            return;

        playerHealth--;

        UpdateUI();

        if (playerHealth <= 0)
            TriggerGameOver("Pesawatmu Hancur!");
    }

    // 🔥 BOSS SPAWN FIX
    private IEnumerator SpawnBossRoutine()
    {
        bossSpawned = true;

        enemySpawner?.SetActive(false);

        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies)
            Destroy(e);

        BossWarningPanel?.SetActive(true);

        float waitTime = 3f;
        if (gameSettings != null)
            waitTime = gameSettings.warningDuration;

        yield return new WaitForSeconds(waitTime);

        BossWarningPanel?.SetActive(false);

        if (gameSettings != null &&
            gameSettings.bossPrefab != null &&
            bossSpawnPoint != null)
        {
            Instantiate(
                gameSettings.bossPrefab,
                bossSpawnPoint.position,
                Quaternion.identity
            );
        }
    }

    // 🔥 CALLED FROM BOSS SCRIPT
    public void BossDefeated()
    {
        if (bossDefeated) return;

        bossDefeated = true;
        TriggerStageClear();
    }

    private void CheckStageResult()
    {
        if (stageCompleted || isGameOver)
            return;

        if (gameSettings != null && gameSettings.hasBoss)
        {
            if (bossDefeated)
                TriggerStageClear();
            else
                TriggerGameOver("Boss belum dikalahkan!");
        }
        else
        {
            if (kills >= minKillsToPass)
                TriggerStageClear();
            else
                TriggerGameOver("Target kill gagal!");
        }
    }

    // 🔥 STAGE CLEAR FIX
    private void TriggerStageClear()
    {
        if (stageCompleted) return;

        stageCompleted = true;

        enemySpawner?.SetActive(false);

        stageClearPanel?.SetActive(true);

        Time.timeScale = 0f;
    }

    private void TriggerGameOver(string reason)
    {
        if (isGameOver) return;

        isGameOver = true;

        enemySpawner?.SetActive(false);

        gameOverPanel?.SetActive(true);

        if (gameOverReasonText != null)
            gameOverReasonText.text = reason;

        Time.timeScale = 0f;
    }

    // 🔥 UI
    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score;

        if (killText != null)
        {
            if (gameSettings != null && gameSettings.hasBoss)
            {
                if (!bossSpawned)
                    killText.text = $"KILLS: {kills} / {gameSettings.killsBeforeBoss}";
                else
                    killText.text = "BOSS FIGHT!";
            }
            else
            {
                killText.text = $"KILLS: {kills} / {minKillsToPass}";
            }
        }

        if (timerText != null)
        {
            string m = Mathf.FloorToInt(timer / 60).ToString("00");
            string s = Mathf.FloorToInt(timer % 60).ToString("00");

            timerText.text = $"TIME: {m}:{s}";
        }

        if (heartIcons != null)
        {
            for (int i = 0; i < heartIcons.Length; i++)
            {
                if (i < playerHealth)
                {
                    heartIcons[i].enabled = true;
                    heartIcons[i].sprite = fullHeartSprite;
                }
                else
                {
                    heartIcons[i].enabled = true;
                    heartIcons[i].sprite = emptyHeartSprite;
                }
            }
        }
    }
}