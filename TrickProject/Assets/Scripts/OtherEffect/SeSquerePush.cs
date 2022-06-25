using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeSquerePush : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("otherItem"))
        {
            Rigidbody2D rigb = collision.GetComponent<Rigidbody2D>();
            rigb.velocity=new Vector2(20, rigb.velocity.y+5f);
        }
    }
}
