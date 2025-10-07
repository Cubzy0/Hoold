using UnityEngine;

public class PlayerDragController2D : MonoBehaviour
{
    public float sensitivity = 1.2f;
    public float xBoundary = 2.6f;
    Vector3 lastWorld;

    Vector3 MouseWorld(){
        var mp = Input.mousePosition;
        mp.z = 10f; // distance to camera
        return Camera.main.ScreenToWorldPoint(mp);
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)) lastWorld = MouseWorld();
        if (Input.GetMouseButton(0)){
            var now = MouseWorld();
            float deltaX = (now.x - lastWorld.x) * sensitivity;
            transform.position += new Vector3(deltaX, 0f, 0f);
            var p = transform.position;
            p.x = Mathf.Clamp(p.x, -xBoundary, xBoundary);
            transform.position = p;
            lastWorld = now;
        }
    }
}
