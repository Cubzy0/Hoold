using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager2D : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    [Header("Refs")]
    public PlayerDragController2D playerController;
    public Spawner2D spawner;

    int score;
    bool isGameOver;

    void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (gameOverPanel) gameOverPanel.SetActive(false);
        UpdateScoreHUD();

        // Auto-wire if you forgot to set refs in Inspector
        if (!playerController) playerController = FindObjectOfType<PlayerDragController2D>();
        if (!spawner)         spawner         = FindObjectOfType<Spawner2D>();
    }

    public void AddScore(int a)
    {
        if (isGameOver) return;
        score += a;
        UpdateScoreHUD();
    }

    void UpdateScoreHUD() { if (scoreText) scoreText.text = score.ToString(); }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // 1) Stop gameplay scripts
        if (playerController) playerController.enabled = false;
        if (spawner)         spawner.enabled         = false;
        foreach (var s in FindObjectsOfType<Scroller2D>()) s.enabled = false;

        // 2) Freeze time + audio (anything using unscaled time will also stop because we disabled scripts)
        Time.timeScale = 0f;
        AudioListener.pause = true;

        // 3) Show overlay
        if (finalScoreText) finalScoreText.text = $"Score: {score}";
        if (gameOverPanel)  gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
