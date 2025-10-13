using UnityEngine;

public class Spawner2D : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] spawnables;       // Coin, Hazard, etc.

    [Header("Spawn Timing")]
    public float baseInterval = 1.25f;    // easy start
    public float minInterval  = 0.35f;    // fastest we allow
    public float difficultyPerMeter = 0.004f; // how quickly it gets harder

    [Header("Spawn Area")]
    public float xRange = 2.6f;
    public float ySpawn = 6f;

    float timer;

    // Current target interval based on distance
    float CurrentInterval()
    {
        // Distance comes from your GameManager (static getter)
        float d = GameManager2D.Distance; // meters
        float interval = baseInterval - (d * difficultyPerMeter);
        return Mathf.Max(minInterval, interval);
    }

    void Update()
    {
        timer += Time.deltaTime;  // uses scaled time -> slow-mo also slows spawning (feels coherent)
        if (timer >= CurrentInterval())
        {
            timer = 0f;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        if (spawnables == null || spawnables.Length == 0) return;

        float x = Random.Range(-xRange, xRange);
        var prefab = spawnables[Random.Range(0, spawnables.Length)];
        Instantiate(prefab, new Vector3(x, ySpawn, 0f), Quaternion.identity);
    }
}
