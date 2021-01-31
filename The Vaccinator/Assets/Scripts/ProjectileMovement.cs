using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    bool hitWall;
    public float lifetime;
    float lifeTimer;
    AudioSource source;
    public AudioClip[] shots;
    public AudioClip[] hits;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        lifeTimer = lifetime;
        source = GetComponent<AudioSource>();
        source.clip = shots[Random.Range(0, shots.Length)];
        source.Play();
    }

    void Update()
    {
        if (!hitWall)
        {
            Vector2 dir = rb.velocity;
            if (!dir.Equals(Vector3.zero))
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        lifeTimer -= Time.deltaTime;
        if (lifeTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6  /* Ground layer */ || other.gameObject.layer == 7  /* Enemy layer */)
        {
            GetComponent<Collider2D>().enabled = false;
            Destroy(rb);
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            hitWall = true;
            transform.SetParent(other.transform);
        }

        if (other.CompareTag("enemy"))
        {
            source.clip = hits[Random.Range(0, hits.Length)];
            source.volume = 0.5f;
            source.Play();
            other.GetComponent<EnemyHealth>().Hit();
        }
    }
}
