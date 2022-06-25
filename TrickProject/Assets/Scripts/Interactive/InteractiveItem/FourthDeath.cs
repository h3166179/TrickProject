using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthDeath : InteractiveItem
{
    private GameObject catDeath;
    private Transform knifePoint;
    [SerializeField] private FourthKnife fourthKnife;
    [SerializeField] private float knifeSpeed = 0.2f;

    protected override void Start()
    {
        base.Start();
        catDeath = transform.Find("deathPoint").gameObject;
        knifePoint = transform.Find("knifePoint");
        GameManager.Instance.RegisterCatDeathList(catDeath);
    }

    protected override void EnterDelegate()
    {
        //猫猫尸体脱落
        fourthKnife.isPush = true;
        StartCoroutine(PushKnife());
        
        //catDeath.transform.SetParent(transform.parent);
    }

    private IEnumerator PushKnife()
    {
        catDeath.SetActive(true);
        catDeath.transform.position = GameManager.Instance.GetPlayer().position;
        //TODO:猫猫重生
        float time =0;
        Vector2 ori_position = fourthKnife.transform.position;
        while (time<1)
        {
            time += Time.deltaTime / knifeSpeed;
            fourthKnife.transform.position=Vector2.Lerp(ori_position, knifePoint.position, time);
            //TODO:Maybe需要修改层级
            yield return null;
        }
        //精度修正
        fourthKnife.transform.position = knifePoint.position;
        yield return null;
        fourthKnife.SetKnifeCol(false);
    }
}
