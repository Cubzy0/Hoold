using UnityEngine;

public class Hazard2D : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c){
        if (c.collider.CompareTag("Player")){
            FindObjectOfType<GameManager2D>()?.GameOver();
        }
    }
}
