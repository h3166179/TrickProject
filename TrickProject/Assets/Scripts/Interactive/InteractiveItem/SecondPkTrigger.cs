using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SecondPkTrigger : InteractiveItem
{
    [SerializeField] private Transform transform;

    protected override void EnterDelegate()
    {
        transform.DORotate(new Vector3(0,0,-60f),0.3f);
        //TODO:暂时消失
        GetComponent<Collider2D>().enabled = false;
    }
}
