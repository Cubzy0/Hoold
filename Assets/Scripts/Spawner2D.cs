using UnityEngine;

public class Spawner2D : MonoBehaviour
{
    public GameObject[] spawnables; // assign Coin, HazardBar
    public float interval = 1.1f;
    public float xRange = 2.6f;
    float t;

    void Update(){
        t += Time.deltaTime;
        if (t >= interval){
            t = 0f;
            var prefab = spawnables[Random.Range(0, spawnables.Length)];
            float x = Random.Range(-xRange, xRange);
            Instantiate(prefab, new Vector3(x, 6f, 0f), Quaternion.identity);
        }
    }
}
