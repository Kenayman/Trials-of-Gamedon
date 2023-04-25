using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    [SerializeField] private GameObject[] background;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform lastPoint;
    [SerializeField] private int initialCount;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i<initialCount; i++)
        {
            GenerateLevel();
        }
    }
    private void Update()
    {
        if(Vector2.Distance(player.position, lastPoint.position) < minDistance)
        {
            GenerateLevel();
        }
    }

    private void GenerateLevel()
    {
        int randNum = Random.Range(0, background.Length);
        GameObject level = Instantiate(background[randNum], lastPoint.position, Quaternion.identity);
        lastPoint = FindLastPoint(level, "FinalPoint");
    }

    private Transform FindLastPoint(GameObject levelPart, string sticker)
    {
        Transform point = null;

        foreach(Transform ubication in levelPart.transform)
        {
            if (ubication.CompareTag(sticker))
            {
                point = ubication;
                break;
            }
        }

        return point;
    }

}
