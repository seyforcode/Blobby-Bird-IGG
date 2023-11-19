using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float maxTime = 1f;

    [SerializeField] private float spawnYRange = 1f;

    private float timer = 0;


    private void Update()
    {
        if(!UIController.gameStarted) return;
        
        if (timer > maxTime)
        {
            SpawnObstacle();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        obstacle.transform.position += Vector3.up * Random.Range(-spawnYRange, spawnYRange);
        Destroy(obstacle,6f);
    }
}
