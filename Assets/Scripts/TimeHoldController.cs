using UnityEngine;
using UnityEngine.UI; // only needed if you wired a UI Image

public class TimeHoldController : MonoBehaviour
{
    [Header("Slow Motion")]
    [Range(0.05f, 0.9f)] public float slowScale = 0.3f;
    public float lerpSpeed = 6f;
    public static bool IsSlowed { get; private set; }

    [Header("Hold Limits")]
    public float maxHoldTime = 3f;   // max continuous slow-mo time
    public float refillRate  = 1f;   // recharge per second when NOT holding
    private float currentHoldTime = 0f;
    private bool exhausted = false;  // true once meter hits max while holding

    [Header("Optional UI")]
    public Image holdMeterFill;      // set Image to "Filled" type

    void Awake() { Time.timeScale = 1f; }

    void Update()
    {
        // Your existing (old) input method. Works if Active Input Handling = Both/Old
        bool hold = Input.GetMouseButton(0) || Input.touchCount > 0;

        // Reset the lock as soon as the player RELEASES
        if (!hold && exhausted)
            exhausted = false;

        if (hold)
        {
            // Only allow slow-mo if not exhausted and meter not full yet
            if (!exhausted && currentHoldTime < maxHoldTime)
            {
                Time.timeScale = Mathf.MoveTowards(Time.timeScale, slowScale, lerpSpeed * Time.unscaledDeltaTime);
                currentHoldTime += Time.unscaledDeltaTime; // drain with unscaled so it still drains while slowed

                // If we just reached cap WHILE holding, lock it
                if (currentHoldTime >= maxHoldTime - 0.0001f)
                {
                    currentHoldTime = maxHoldTime;
                    exhausted = true; // must release before using slow-mo again
                }
            }
            else
            {
                // Exhausted: force normal speed; DO NOT refill until released
                Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1f, lerpSpeed * Time.unscaledDeltaTime);
                currentHoldTime = maxHoldTime; // keep clamped
            }
        }
        else
        {
            // Not holding: return to normal and refill
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1f, lerpSpeed * Time.unscaledDeltaTime);
            currentHoldTime = Mathf.MoveTowards(currentHoldTime, 0f, refillRate * Time.unscaledDeltaTime);
        }

        currentHoldTime = Mathf.Clamp(currentHoldTime, 0f, maxHoldTime);
        IsSlowed = Time.timeScale < 0.99f;

        // UI fill (1 = full, 0 = empty)
        if (holdMeterFill != null)
            holdMeterFill.fillAmount = 1f - (currentHoldTime / maxHoldTime);
    }
}