using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform1;
    [SerializeField] private Transform spawnTransform2;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private int numEnemiesToSpawn = 5;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] public bool isPlayerAlive = true;


    private bool hasSpawnedEnemies = false;
    private int numEnemiesSpawned = 0;


    private void Start()
    {
        if (!hasSpawnedEnemies )
        {
            StartCoroutine(SpawnEnemiesOverTime());
        }
    }

    private IEnumerator SpawnEnemiesOverTime()
    {
        hasSpawnedEnemies = true;

        while (numEnemiesSpawned < numEnemiesToSpawn)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], spawnTransform1.position, enemyPrefab[enemyIndex].transform.rotation);
            Instantiate(enemyPrefab[enemyIndex], spawnTransform2.position, enemyPrefab[enemyIndex].transform.rotation);
            numEnemiesSpawned++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
