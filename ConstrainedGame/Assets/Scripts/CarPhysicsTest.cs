using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsTest : MonoBehaviour
{
    public float power = 3;
    public float maxspeed = 5;
    public float turnpower = 2;
    public float friction = 3;
    public Vector2 curspeed;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        curspeed = new Vector2(rb.velocity.x, rb.velocity.y);

        if (curspeed.magnitude > maxspeed)
        {
            curspeed = curspeed.normalized;
            curspeed *= maxspeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * power);
            rb.drag = friction;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-(transform.up) * (power / 2));
            rb.drag = friction;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * turnpower);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * -turnpower);
        }

        noGas();

    }

    void noGas()
    {
        bool gas;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            gas = true;
        }
        else
        {
            gas = false;
        }

        if (!gas)
        {
            rb.drag = friction * 2;
        }
    }
}
