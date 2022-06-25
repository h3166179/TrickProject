using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDeath : InteractiveItem
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
        //TODO:猫猫重生
        catDeath.SetActive(true);
        //猫猫失去生命
        GameManager.Instance.UpdateCatHealth(false);
        Destroy(GetComponent<Collider2D>());
    }

}
