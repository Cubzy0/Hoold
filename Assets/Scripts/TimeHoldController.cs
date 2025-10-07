using UnityEngine;

public class TimeHoldController : MonoBehaviour
{
    [Range(0.05f,0.9f)] public float slowScale = 0.3f;
    public float lerpSpeed = 6f;
    public static bool IsSlowed { get; private set; }

    void Awake(){ Time.timeScale = 1f; }
    void Update(){
        bool hold = Input.GetMouseButton(0) || Input.touchCount > 0;
        float target = hold ? slowScale : 1f;
        Time.timeScale = Mathf.MoveTowards(Time.timeScale, target, lerpSpeed * Time.unscaledDeltaTime);
        IsSlowed = Time.timeScale < 0.99f;
    }
}
