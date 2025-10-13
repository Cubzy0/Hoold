using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager2D : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI distanceText;     // NEW: live distance HUD
    public TextMeshProUGUI finalDistanceText; // NEW: Game Over distance

    [Header("Refs")]
    public PlayerDragController2D playerController;
    public Spawner2D spawner;

    int score;
    float distance; // NEW
    bool isGameOver;

    void Start()
    {
        Time.timeScale = 2f;
        AudioListener.pause = false;
        distance = 0f;

        if (gameOverPanel) gameOverPanel.SetActive(false);
        UpdateScoreHUD();

        if (!playerController) playerController = FindObjectOfType<PlayerDragController2D>();
        if (!spawner)         spawner         = FindObjectOfType<Spawner2D>();
    }

    void Update()
    {
        // Only count distance while playing
        if (!isGameOver)
        {
            // Assuming obstacles/coins move down at ~scrollSpeed = 2 units/sec
            float scrollSpeed = 2f;
            distance += scrollSpeed * Time.deltaTime;
            UpdateDistanceHUD();
        }
    }

    public void AddScore(int a)
    {
        if (isGameOver) return;
        score += a;
        UpdateScoreHUD();
    }

    void UpdateScoreHUD()
    {
        if (scoreText) scoreText.text = score.ToString();
    }

    void UpdateDistanceHUD()
    {
        if (distanceText)
            distanceText.text = $"{distance:F1} m";
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (playerController) playerController.enabled = false;
        if (spawner) spawner.enabled = false;
        foreach (var s in FindObjectsOfType<Scroller2D>()) s.enabled = false;

        Time.timeScale = 0f;
        AudioListener.pause = true;

        if (finalScoreText) finalScoreText.text = $"Score: {score}";
        if (finalDistanceText) finalDistanceText.text = $"Distance: {distance:F1} m";
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
