using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    int maxHP;
    public bool replicate;
    public GameObject spawnEnemy;
    SpriteRenderer sr;
    float hitTime = 0.05f;
    float hitTimer = -1;
    EnemySpawner controller;
    Animator score;
    public GameObject hitEffect;
    public AudioClip[] deaths;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        maxHP = health;
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawner>();
        score = GameObject.FindGameObjectWithTag("score").GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0)
        {
            if (replicate)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(spawnEnemy, transform.position + Vector3.left * Random.Range(0, 2f), Quaternion.identity);
                }
            }
            controller.score += maxHP;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().chanceOfDeath -= maxHP / 10f;
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            controller.GetComponent<AudioSource>().clip = deaths[Random.Range(0, deaths.Length)];
            controller.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }

        if (hitTimer >= 0)
        {
            sr.color = Color.red;
        } 
        else
        {
            sr.color = Color.white;
        }
        hitTimer -= Time.deltaTime;
    }

    public void Hit()
    {
        health--;
        controller.score++;
        hitTimer = hitTime;
        score.SetTrigger("change");
    }
}
