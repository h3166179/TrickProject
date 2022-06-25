using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthKnife : InteractiveItem
{
    [SerializeField] private Collider2D knifeCol;
    private bool isPush = false;

    protected override void EnterDelegate()
    {
        base.EnterDelegate();

    }
}
