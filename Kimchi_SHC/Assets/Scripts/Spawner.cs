using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minSpawnDelay = 5f;
    public float maxSpawnDelay = 7f;

    public GameObject[] gameObjects;
    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    // Update is called once per frame
    void Spawn()
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
