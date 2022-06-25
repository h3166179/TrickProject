using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHat : MonoBehaviour
{
    //TODO:帽子层级调高
    private Transform player;
    public Transform loadPoint;
    [SerializeField] private float jumpHeight = 8f;
    [SerializeField] private JumpHat relativeObj;//关联帽子

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        loadPoint = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player.GetComponent<PlayerController>().isJump && collision.tag.Equals("Player"))
        {
            if(relativeObj!=null)
            {
                player.transform.position = relativeObj.loadPoint.position;
                Rigidbody2D rigidbody2D=player.GetComponent<Rigidbody2D>();
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
            }
        }
    }
}
