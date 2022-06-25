using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveShow : InteractiveBase
{
    [SerializeField] private Material outMt;
    [SerializeField] private float tipSpeed = 0.5f;

    private GameObject showTipPb;
    private CanvasGroup showGroup;
    private Material normalMt;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalMt = spriteRenderer.material;
        outMt = Resources.Load<Material>("Shader/ShowMt");
        showTipPb = gameObject.transform.Find("ShowTips").gameObject;
        showGroup = showTipPb.GetComponent<CanvasGroup>();
    }

    protected override void EnterDelegate()
    {
        if (outMt != null)
            spriteRenderer.material = outMt;
        else
            Debug.Log("InteractiveShow Dont Have outMt");
        if (showTipPb != null)
            StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        showTipPb.SetActive(true);
        float time = 0;
        while(time<1)
        {
            time += Time.deltaTime / tipSpeed;
            showGroup.alpha = time;
            yield return null;
        }
        //精度修正
        showGroup.alpha = 1;
    }

    protected override void ExitDelegate()
    {
        if(normalMt!=null)
            spriteRenderer.material = normalMt;
        if (showTipPb != null)
            StartCoroutine(HideText());
    }

    private IEnumerator HideText()
    {
        float time = 1;
        while (time >0)
        {
            time -= Time.deltaTime / tipSpeed;
            showGroup.alpha = time;
            yield return null;
        }
        //精度修正
        showGroup.alpha = 0;
        showTipPb.SetActive(false);
    }
}
