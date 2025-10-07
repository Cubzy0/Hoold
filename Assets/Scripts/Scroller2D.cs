using UnityEngine;

public class Scroller2D : MonoBehaviour
{
    public Vector2 direction = Vector2.down;  // moves toward player
    public float extraSpeed = 0f;

    void Update(){
        transform.Translate((direction * (WorldScrollController2D.Speed + extraSpeed)) * Time.deltaTime);
        if (transform.position.y < -12f) Destroy(gameObject); // cleanup
    }
}
