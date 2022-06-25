using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHat : MonoBehaviour
{
    //TODO:帽子层级调高
    private Transform player;
    public Transform targetPoint;
    public bool isTrans = false;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private JumpHat relativeObj;//关联帽子

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        //loadPoint = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrans)
            return;
        if(!player.GetComponent<PlayerController>().isJump && collision.tag.Equals("Player"))
        {
            isTrans = true;
            if (relativeObj != null)
            {
                relativeObj.isTrans = true;
                GetComponent<Collider2D>().enabled = false;
                if (player != null)
                    player.transform.position = targetPoint.position;
                Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
                Invoke("WaitTime", 2f);
            }
        }
    }

    private void WaitTime()
    {
        isTrans = false;
        relativeObj.isTrans = false;
        GetComponent<Collider2D>().enabled = true;
    }
}
