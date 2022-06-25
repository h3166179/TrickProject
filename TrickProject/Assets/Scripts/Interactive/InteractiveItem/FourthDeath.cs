using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthDeath : InteractiveItem
{
    private GameObject catDeath;
    [SerializeField]private Transform knifePoint;
    [SerializeField] private FourthKnife fourthKnife;
    [SerializeField] private float knifeSpeed = 0.2f;

    protected override void Start()
    {
        base.Start();
        catDeath = transform.Find("DeadPoint").gameObject;
        //knifePoint = transform.Find("knifePoint");
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
        //catDeath.transform.position = GameManager.Instance.GetPlayer().position;
        GameManager.Instance.GetPlayer().gameObject.SetActive(false);
        fourthKnife.transform.parent.GetComponent<Collider2D>().enabled = true;
        GameManager.Instance.ResetDead(2, GameManager.Instance.GetPlayer(), null, 3, false);
        GameManager.Instance.HealthUIUpdate();
        DialogManager.Instance.DialogPlay(3);
        catDeath.SetActive(true);
        float time =0;
        Vector2 ori_position = fourthKnife.transform.parent.position;
        while (time<1)
        {
            time += Time.deltaTime / knifeSpeed;
            fourthKnife.transform.parent.position = Vector2.Lerp(ori_position, knifePoint.position, time);
            //TODO:Maybe需要修改层级
            yield return null;
        }
        //精度修正
        fourthKnife.transform.parent.position = knifePoint.position;
        yield return null;
        //fourthKnife.SetKnifeCol(false);
    }
}
