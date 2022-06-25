using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SecondPkTrigger : InteractiveItem
{
    //[SerializeField] private Transform transform;
    [SerializeField] private Animator boardAnim;

    protected override void EnterDelegate()
    {
        //transform.DORotate(new Vector3(0,0,-60f),0.3f);
        GetComponent<Animator>().SetBool("isTrigger",true);
        GetComponent<Collider2D>().enabled = false;
        Invoke("HideBoard", 1.5f);
    }

    private void HideBoard()
    {
        boardAnim.SetBool("isTrigger",true);
    }
}
