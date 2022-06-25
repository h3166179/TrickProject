using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveShow : InteractiveBase
{
    [SerializeField] private Material outMt;

    private GameObject showTipPb;
    private Material normalMt;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalMt = spriteRenderer.material;
        outMt = Resources.Load<Material>("Shader/ShowMt");
        showTipPb = gameObject.transform.Find("ShowTips").gameObject;
    }

    protected override void EnterDelegate()
    {
        if (outMt != null)
            spriteRenderer.material = outMt;
        else
            Debug.Log("InteractiveShow Dont Have outMt");
        if(showTipPb!=null)
            showTipPb.SetActive(true);
    }

    protected override void ExitDelegate()
    {
        if(normalMt!=null)
            spriteRenderer.material = normalMt;
        if (showTipPb != null)
            showTipPb.SetActive(false);
    }
}
