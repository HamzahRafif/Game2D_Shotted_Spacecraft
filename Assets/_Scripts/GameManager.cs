using UnityEngine;
using TMPro; // Wajib untuk TextMeshPro
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Stage Data")]
    public StageData currentStageData;

    [Header("Stage Settings")]
    private int currentStage;
    private int minKillsToPass;
    private float timer;
    private bool stageCompleted = false;

    [Header("Player Stats")]
    public int playerHealth = 3;
    public int score = 0;
    public int kills = 0;
    public int credits = 0;
    public bool isGameOver = false;

    [Header("UI References (Text)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killText;

    [Header("UI Windows (Tambahan Baru)")]
    // Referensi untuk objek UI Panel yang akan muncul saat Kalah/Menang
    public GameObject gameOverPanel;
    public GameObject stageClearPanel;
    public TextMeshProUGUI gameOverReasonText; // Untuk menampilkan alasan kalah (opsional)

    [Header("UI HP Player")]
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
        // Pastikan waktu game berjalan normal saat mulai
        Time.timeScale = 1f; 
        
        if(currentStageData != null)
{
        currentStage = currentStageData.stageNumber;
        minKillsToPass = currentStageData.targetKills;
        timer = currentStageData.stageDuration;
    }
        // Menyembunyikan panel UI saat game dimulai
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (stageClearPanel != null) stageClearPanel.SetActive(false);

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

        // KONDISI BARU: Cek langsung jika kill sudah mencapai target 25
        if (kills >= minKillsToPass)
        {
            TriggerStageClear();
        }
    }

    public void TakeDamage()
    {
        if (isGameOver || stageCompleted) return;

        playerHealth--;
        UpdateUI();

        if (playerHealth <= 0)
        {
            TriggerGameOver("Pesawatmu Hancur!");
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
        
                // Mengulang pengecekan sebanyak ukuran array heartIcons (yaitu 3 kali)
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < playerHealth)
            {
                // Jika indeks iterasi lebih kecil dari sisa darah, tampilkan hati penuh
                heartIcons[i].enabled = true;
                if (fullHeartSprite != null) heartIcons[i].sprite = fullHeartSprite;
            }
            else
            {
                // Jika player menerima damage dan indeks melebihi sisa darah:
                if (emptyHeartSprite != null)
                {
                    heartIcons[i].sprite = emptyHeartSprite;
                }
                else
                {
                    // Jika aset gambar kosong tidak dipasang, objek UI hati langsung dinonaktifkan dari layar
                    heartIcons[i].enabled = false;
                }
            }
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
        // Fungsi ini dipicu jika waktu habis (timer == 0)
        if (kills >= minKillsToPass)
        {
            TriggerStageClear();
        }
        else
        {
            TriggerGameOver("Waktu Habis! Target Kill Gagal Terpenuhi.");
        }
    }

    // FUNGSI BARU: Dipanggil saat berhasil menyelesaikan stage
    void TriggerStageClear()
    {
        stageCompleted = true;
        Debug.Log("STAGE 1 CLEAR! Target terpenuhi.");
        
        if (enemySpawner != null) enemySpawner.SetActive(false);

        // Memunculkan UI Stage Clear
        if (stageClearPanel != null) stageClearPanel.SetActive(true);

        // Menghentikan pergerakan game (Stage Terhenti)
        Time.timeScale = 0f; 
    }

    void TriggerGameOver(string reason)
    {
        isGameOver = true;
        Debug.Log("GAME OVER: " + reason);
        
        if (enemySpawner != null) enemySpawner.SetActive(false);

        // Memunculkan UI Game Over
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        
        // Menampilkan teks alasan kalah jika ada komponennya
        if (gameOverReasonText != null) gameOverReasonText.text = reason;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && playerHealth <= 0)
        {
            player.SetActive(false);
        }

        // Menghentikan pergerakan game (Stage Terhenti)
        Time.timeScale = 0f; 
    }
}