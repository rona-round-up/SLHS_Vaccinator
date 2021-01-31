using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float chanceOfDeath = 0;
    public SpriteRenderer[] renderers;
    public float invulTime;

    bool invul;
    float invulTimer;

    public GameObject hurtEffect;

    public AudioSource source;
    public AudioClip[] deaths;
    public AudioClip[] hits;
    public Text cod;

    public GameObject loseScreen;

    void Start()
    {
        Time.timeScale = 1;
        Physics2D.IgnoreLayerCollision(8, 7, false);
        invul = false;
        invulTimer = invulTime;
    }

    void Update()
    {
        if (chanceOfDeath < 0)
        {
            chanceOfDeath = 0;
        }
        if (invul)
        {
            invulTimer -= Time.deltaTime;
            if (invulTimer < 0)
            {
                EndInvul();
            }
        }
        cod.text = (int)chanceOfDeath + "%";
    }

    public void TakeDamage()
    {
        if (!invul)
        {
            Instantiate(hurtEffect, transform.position, Quaternion.identity);
            source.clip = hits[Random.Range(0, hits.Length)];
            source.Play();
            chanceOfDeath += 5;
            MakeInvul();
        }

        if (Random.Range(0, 101) < chanceOfDeath)
        {
            source.clip = deaths[Random.Range(0, deaths.Length)];
            source.Play();
            loseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void MakeInvul()
    {
        invul = true;
        invulTimer = invulTime;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = new Color(renderers[i].color.r, renderers[i].color.g, renderers[i].color.b, 0.5f);
        }
        Physics2D.IgnoreLayerCollision(8, 7);
    }

    void EndInvul()
    {
        invul = false;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = new Color(renderers[i].color.r, renderers[i].color.g, renderers[i].color.b, 1f);
        }
        Physics2D.IgnoreLayerCollision(8, 7, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            TakeDamage();
        }
    }
}
