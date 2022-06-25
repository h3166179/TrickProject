using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Item"))
        {
            this.gameObject.SetActive(false);
        }
        //TODO:方案2 直接撞飞就好了 设置碰撞层级
    }
}
