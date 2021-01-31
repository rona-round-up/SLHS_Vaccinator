using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    GameObject player;
    Vector3 distanceFromPointer;
    float rotZ;
    float rotX;
    float offset;

    void Start()
    {
        offset = 0;
        player = transform.parent.gameObject;
    }

    void FixedUpdate()
    {
        distanceFromPointer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(distanceFromPointer.y, distanceFromPointer.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.parent.position.x)
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            offset = -1;
            rotX = 180;
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            offset = 1;
            rotX = 0;
        }
        rotZ *= offset;

        transform.rotation = Quaternion.Euler(rotX, 0, rotZ);
    }
}

