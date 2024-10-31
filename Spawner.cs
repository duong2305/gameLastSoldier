using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public float spawnRate = 1000000.0f; // Adjusted to spawn every 3 seconds

    [SerializeField] private GameObject[] enemyPrefab;

    [SerializeField] private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnerR());
    }

    void Update()
    {
        // You can add other update logic here if needed
    }

    private IEnumerator SpawnerR()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            Debug.Log("Spawning enemy"); // Add this line for debugging

            yield return wait;
            int rand = Random.Range(0, enemyPrefab.Length);
            GameObject enemyToSpawn = enemyPrefab[rand];

            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }
}
