using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthKnife : InteractiveItem
{
    [SerializeField] private Collider2D knifeCol;
    public bool isPush = false;

    protected override void EnterDelegate()
    {
        base.EnterDelegate();
        if(!isPush)
        {
            //TODO:无效死亡复活
        }
    }

    public void SetKnifeCol(bool isTrigger)
    {
        knifeCol.isTrigger = isTrigger;
    }
}
