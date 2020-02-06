using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public float acceleration, steering;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float horiz = -Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        body.AddForce(transform.up * (vert * acceleration * Time.fixedDeltaTime));

        if (Vector2.Dot(body.velocity, body.GetRelativeVector(Vector2.up)) >= 0.0f)
        {
            body.rotation += horiz * steering * (body.velocity.magnitude / 5.0f);
        }
        else
        {
            body.rotation -= horiz * steering * (body.velocity.magnitude / 5.0f);
        }

        Vector2 perpendicular = Quaternion.AngleAxis(body.angularVelocity > 0 ? -90 : 90, Vector3.forward) * new Vector2(0.0f, 0.5f);

        body.AddForce(body.GetRelativeVector(-perpendicular.normalized * (Vector2.Dot(body.velocity, body.GetRelativeVector(perpendicular.normalized))) * 0.3f));
    }


    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(body.velocity, transform.up);
    }

    Vector2 RightVelocity()
    {
        return transform.up * Vector2.Dot(body.velocity, transform.right);
    }


}
