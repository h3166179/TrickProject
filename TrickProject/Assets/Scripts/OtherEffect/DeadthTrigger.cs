using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadthTrigger : MonoBehaviour
{
    [SerializeField] private int index;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            GameManager.Instance.ResetDead(0,GameManager.Instance.GetPlayer(),null,index,false);
        }
    }
}
