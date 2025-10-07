using UnityEngine;

public class Collectible2D : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            FindObjectOfType<GameManager2D>()?.AddScore(1);
            Destroy(gameObject);
        }
    }
}
