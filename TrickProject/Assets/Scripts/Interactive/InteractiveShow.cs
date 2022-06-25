using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveShow : InteractiveBase
{
    [SerializeField] private Material outMt;

    private Material normalMt;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalMt = spriteRenderer.material;
        //TODO:outMt动态加载
    }

    protected override void EnterDelegate()
    {
        if (outMt != null)
            spriteRenderer.material = outMt;
        else
            Debug.Log("InteractiveShow Dont Have outMt");
    }

    protected override void ExitDelegate()
    {
        if(normalMt!=null)
        {
            spriteRenderer.material = normalMt;
        }
    }
}
