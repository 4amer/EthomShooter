using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditorInternal;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float timeToSpawnConst;

    private float timeToSpawn;

    [SerializeField] private GameObject spawnEffect;
    private void Start()
    {
        timeToSpawn = timeToSpawnConst;
    }

    private void Update()
    {
        int randInt = Random.Range(0,spawnPoints.Length - 1);
        Transform pointTransform = spawnPoints[randInt].transform;
        if (timeToSpawn < 0)
        {
            Instantiate(enemy, pointTransform.position, pointTransform.rotation);
            Instantiate(spawnEffect, pointTransform.position, pointTransform.rotation);
            timeToSpawn = timeToSpawnConst;
        }
        timeToSpawn -= Time.deltaTime;
    }
}
