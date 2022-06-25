﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDeath : InteractiveItem
{
    //private GameObject catDeath;

    protected override void Start()
    {
        base.Start();
        //catDeath = transform.Find("DeadPoint").gameObject;
        //GameManager.Instance.RegisterCatDeathList(catDeath);
    }

    protected override void EnterDelegate()
    {
        //catDeath.SetActive(true);
        //catDeath.transform.position = GameManager.Instance.GetPlayer().position;
        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().SetDead();
        GameManager.Instance.ResetDead(2,GameManager.Instance.GetPlayer(),null,0);
        //播放文字
        DialogManager.Instance.DialogPlay(0);
        //猫猫失去生命
        GameManager.Instance.HealthUIUpdate();
        Destroy(GetComponent<Collider2D>());
    }

}
