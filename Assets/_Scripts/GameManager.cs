using UnityEngine;
using TMPro; // Wajib untuk TextMeshPro
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Stage Settings")]
    public int currentStage = 1;
    public float stageDuration = 60f;
    public int minKillsToPass = 25;
    private float timer;
    private bool stageCompleted = false;

    [Header("Player Stats")]
    public int playerHealth = 3;
    public int score = 0;
    public int kills = 0;
    public int credits = 0;
    public bool isGameOver = false;

    [Header("UI References (Text)")]
    // PERUBAHAN DI SINI: Menggunakan TextMeshProUGUI khusus untuk teks Canvas
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killText;

    [Header("= SLOT TUGAS TEMAN: UI HEARTS =")]
    public Image[] heartIcons;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    [Header("Spawner Reference")]
    public GameObject enemySpawner;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        timer = stageDuration;
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver || stageCompleted) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            timer = 0;
            UpdateUI();
            CheckStageResult();
        }
    }

    public void AddKillAndScore(int points)
    {
        if (isGameOver || stageCompleted) return;
        score += points;
        kills++;
        UpdateUI();
    }

    public void TakeDamage()
    {
        if (isGameOver || stageCompleted) return;

        playerHealth--;
        UpdateUI();

        if (playerHealth <= 0)
        {
            TriggerGameOver("Pesawatmu Hancur Lebur!");
        }
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "SCORE: " + score;
        if (killText != null) killText.text = "KILLS: " + kills + " / " + minKillsToPass;

        if (timerText != null)
        {
            string minutes = Mathf.FloorToInt(timer / 60).ToString("00");
            string seconds = Mathf.FloorToInt(timer % 60).ToString("00");
            timerText.text = "TIME: " + minutes + ":" + seconds;
        }

        if (heartIcons != null && heartIcons.Length > 0)
        {
            for (int i = 0; i < heartIcons.Length; i++)
            {
                if (i < playerHealth)
                {
                    heartIcons[i].enabled = true;
                    if (fullHeartSprite != null) heartIcons[i].sprite = fullHeartSprite;
                }
                else
                {
                    if (emptyHeartSprite != null)
                    {
                        heartIcons[i].sprite = emptyHeartSprite;
                    }
                    else
                    {
                        heartIcons[i].enabled = false;
                    }
                }
            }
        }
    }

    void CheckStageResult()
    {
        if (kills >= minKillsToPass)
        {
            stageCompleted = true;
            Debug.Log("STAGE 1 CLEAR! Target terpenuhi.");
            if (enemySpawner != null) enemySpawner.SetActive(false);
        }
        else
        {
            TriggerGameOver("Waktu Habis! Target Kill Gagal Terpenuhi.");
        }
    }

    void TriggerGameOver(string reason)
    {
        isGameOver = true;
        Debug.Log("GAME OVER: " + reason);
        if (enemySpawner != null) enemySpawner.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && playerHealth <= 0)
        {
            player.SetActive(false);
        }
    }
}