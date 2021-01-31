using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 4;
    float dir = 1;
    float dirSmooth = 1;
    public bool launch;
    public float launchForce;
    public float launchForceHorizontal;
    float distFromPlayer;
    public float launchDist;
    bool grounded;
    bool readyToLaunch = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(7, 7);
    }

    private void Update()
    {
        dirSmooth += (dir - dirSmooth) * Time.deltaTime;
        if (transform.position.x < player.position.x)
        {
            dir = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x > player.position.x)
        {
            dir = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        distFromPlayer = Mathf.Abs(player.position.x - transform.position.x);
        if (distFromPlayer < launchDist && grounded)
        {
            grounded = false;
            readyToLaunch = true;
        }
    }

    void FixedUpdate()
    {
        if (readyToLaunch)
        {
            readyToLaunch = false;
            rb.velocity = new Vector2(launchForceHorizontal * dir, launchForce);
        } 
        else if (grounded)
        {
            rb.velocity = new Vector2(dirSmooth * speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 6 /* Ground */)
        {
            grounded = true;
        }
    }
}