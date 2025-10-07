using UnityEngine;
using TMPro;

public class GameManager2D : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int score;

    void Start(){ UpdateScore(); }

    public void AddScore(int a){ score += a; UpdateScore(); }
    void UpdateScore(){ if(scoreText) scoreText.text = score.ToString(); }

    public void GameOver(){
        Debug.Log("Game Over");
        // later: show panel, restart, etc.
    }
}
