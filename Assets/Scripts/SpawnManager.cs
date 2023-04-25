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

    private bool hasSpawnedEnemies = false;


    private void Start()
    {
        if (!hasSpawnedEnemies)
        {
            SpawnEnemies();
        }
    }


    private void SpawnEnemies()
    {
        for (int i = 0; i < numEnemiesToSpawn / 2; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);

            Instantiate(enemyPrefab[enemyIndex], spawnTransform1.position, enemyPrefab[enemyIndex].transform.rotation);
            Instantiate(enemyPrefab[enemyIndex], spawnTransform2.position, enemyPrefab[enemyIndex].transform.rotation);

            
        }

        hasSpawnedEnemies = true;
    }

}