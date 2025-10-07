using UnityEngine;

public class WorldScrollController2D : MonoBehaviour
{
    public float baseSpeed = 6f, ramp = 0.015f, maxSpeed = 15f;
    public static float Speed { get; private set; }

    void Update(){
        Speed = Mathf.Min(maxSpeed, baseSpeed + ramp * Time.unscaledTime);
    }
}
