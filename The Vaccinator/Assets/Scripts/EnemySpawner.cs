using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spanwers;
    public GameObject[] enemies;
    float spawnIndex = 0.5f;
    public int score;
    public float spawnRate = 2;
    float spawnTimer;

    void Start()
    {
        spawnTimer = spawnRate;
    }

    void Update()
    {
        if (score >= 25) {
            spawnIndex = 0.85f;
        }
        if (score >= 75)
        {
            spawnIndex = 0.925f;
        }
        if (score >= 100)
        {
            spawnRate = 1.5f;
        }
        if (score >= 150)
        {
            spawnIndex = 1f;
        }
        if (score >= 450)
        {
            spawnRate = 1.25f;
        }
        if (score >= 700)
        {
            spawnRate = 1f;
        }
        if (score >= 1200)
        {
            spawnRate = 0.75f;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            float rand = Random.Range(0f, spawnIndex);
            int index;
            if (rand < 0.5f)
            {
                index = 0;
            } 
            else if (rand < 0.85f)
            {
                index = 1;
            }
            else if (rand < 0.925f)
            {
                index = 2;
            }
            else
            {
                index = 3;
            }
            Instantiate(enemies[index], index != 1 ? spanwers[Random.Range(0, spanwers.Length)].position : new Vector3(Random.Range(-8, 18), 4, 0), Quaternion.identity);
            spawnTimer = spawnRate;
        }
    }
}
