using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoard : InteractiveItem
{
    protected override void EnterDelegate()
    {
        this.gameObject.SetActive(false);
        //TODO:方案2 直接撞飞就好了 设置碰撞层级
    }

}
