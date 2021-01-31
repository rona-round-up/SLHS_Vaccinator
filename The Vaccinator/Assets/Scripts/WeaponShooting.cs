using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public float fireRate;
    public float projectileSpreadRange;
    public GameObject projectile;

    float fireTimer = 0;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && fireTimer < 0)
        {
            Instantiate(projectile, transform.position, Quaternion.Euler(0, transform.parent.parent.eulerAngles.y, transform.eulerAngles.z + Random.Range(-projectileSpreadRange, projectileSpreadRange)));
            fireTimer = fireRate;
        }
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("shoot", true);
        }
        else
        {
            anim.SetBool("shoot", false);
        }
    }
}
