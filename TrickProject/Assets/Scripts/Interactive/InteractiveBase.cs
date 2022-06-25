using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class InteractiveBase : MonoBehaviour
{
    private Action EnterAction;
    private Action ExitAction;

    private void Start()
    {
        EnterAction += EnterDelegate;
        ExitAction += ExitDelegate;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            EnterAction?.Invoke();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            ExitAction?.Invoke();
        }
    }

    protected virtual void EnterDelegate(){}

    protected virtual void ExitDelegate(){}
}
