using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdDeath : InteractiveItem
{
    private GameObject catDeath;

    protected override void Start()
    {
        base.Start();
        catDeath = transform.GetChild(0).gameObject;
        GameManager.Instance.RegisterCatDeathList(catDeath);
    }

    protected override void EnterDelegate()
    {
        catDeath.SetActive(true);
        catDeath.transform.position = GameManager.Instance.GetPlayer().position;
        //TODO:猫猫重生
        catDeath.transform.SetParent(transform.parent);
    }
}
