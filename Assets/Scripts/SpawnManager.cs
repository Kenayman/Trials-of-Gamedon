using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInter;
}
public class SpawnManager : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] public bool isPlayerAlive;
    public Transform[] SpawnPoint;
    
    public Animator animator;
    private Wave currentWave;
    private int currentWaveInt;
    private bool canSpawn = true;
    private float nextSpawnTime;
    private bool canAnimate = false;
    public Text nextWave;
    private void Update()
    {
        currentWave= waves[currentWaveInt];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(totalEnemies.Length == 0)
        {
            if (currentWaveInt + 1 != waves.Length)
            {
                if(canAnimate == true)
                {
                    animator.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
                
            }
            else
            {
                Debug.Log("Time to Rest");
                animator.SetTrigger("Next");

            }


        }

    }

    private void SpawnWave()
    {
        if (canSpawn && nextSpawnTime< Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = SpawnPoint[Random.Range(0, SpawnPoint.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInter;
            if(currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }


    }

    void SpawnNextWave()
    {
        currentWaveInt++;
        canSpawn = true;
    }

}
