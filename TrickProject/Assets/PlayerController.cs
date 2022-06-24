using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    public float height;

    public float Speed;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, height);
        }

        rigidbody2D.velocity = new Vector2(h*Speed, rigidbody2D.velocity.y);
    }
}
