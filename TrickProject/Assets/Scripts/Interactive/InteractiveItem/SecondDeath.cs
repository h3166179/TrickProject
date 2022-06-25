using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDeath : InteractiveItem
{
    //private GameObject catDeath;

    protected override void Start()
    {
        base.Start();
        //catDeath = transform.GetChild(0).gameObject;
        //GameManager.Instance.RegisterCatDeathList(catDeath);
    }

    protected override void EnterDelegate()
    {
        //猫猫尸体脱落
        //catDeath.SetActive(true);
        //catDeath.transform.position = GameManager.Instance.GetPlayer().position;
        GameManager.Instance.ResetDead(1f,GameManager.Instance.GetPlayer(),null,1);
        DialogManager.Instance.DialogPlay(1);
        //catDeath.transform.SetParent(transform.parent);
    }
}
