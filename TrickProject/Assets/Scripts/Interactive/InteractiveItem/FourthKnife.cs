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
            GameManager.Instance.GetPlayer().GetComponent<PlayerController>().SetDead();
            GameManager.Instance.ResetDead(2f,GameManager.Instance.GetPlayer(),null,3,false);
        }
    }

    public void SetKnifeCol(bool isTrigger)
    {
        knifeCol.isTrigger = isTrigger;
    }
}
