using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager2D : MonoBehaviour
{
    // Global distance everyone reads
    public static float Distance { get; private set; }

    [Header("UI (HUD)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI distanceText;

    [Header("UI (Game Over Panel)")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalDistanceText;

    [Header("Refs")]
    public PlayerDragController2D playerController;
    public Spawner2D spawner;

    int score;
    bool isGameOver;

    void Start()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        Distance = 0f;                         // reset each run
        if (distanceText) distanceText.text = "0.0 m";
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        // Use the SAME speed your scrollers use
        float speed = WorldScrollController2D.Speed;   // or const like 2f
        Distance += speed * Time.deltaTime;            // (use unscaledDeltaTime if you want to ignore slow-mo)

        if (distanceText) distanceText.text = $"{Distance:F1} m";
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        if (scoreText) scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // stop scripts/time
        if (playerController) playerController.enabled = false;
        if (spawner) spawner.enabled = false;
        foreach (var s in FindObjectsByType<Scroller2D>(FindObjectsSortMode.None)) s.enabled = false;

        // write the FINAL numbers BEFORE restart
        if (finalScoreText)    finalScoreText.text    = $"Score: {score}";
        if (finalDistanceText) finalDistanceText.text = $"Distance: {Distance:F1} m";

        if (gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}