using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonBlank<GameManager>
{
    private Transform player;
    //猫生命条
    private Canvas CatHealth;
    private Text healText;
    private List<GameObject> catDeathList = new List<GameObject>();

    public void RegisterPlayer(Transform player)
    {
        this.player = player;
    }

    public void RegisterCatHealth(Canvas catHealth)
    {
        this.CatHealth = catHealth;
        healText = CatHealth.transform.GetChild(0).Find("Heal_Text").GetComponent<Text>();
    }

    public void RegisterCatDeathList(GameObject cat)
    {
        if (!catDeathList.Contains(cat))
            catDeathList.Add(cat);
    }

    //更新猫生命值
    public void UpdateCatHealth(bool isAdd)
    {
        if(healText!=null)
        {
            int new_health;
            if (isAdd)
            {
                new_health = int.Parse(healText.text) + 1;
            }
            else
            {
                new_health = int.Parse(healText.text) - 1;
            }
            healText.text = new_health.ToString();
        }
    }

    //根据序列获取猫猫尸体
    public GameObject GetCatDeath(int index)
    {
        if (catDeathList.Count > index && catDeathList[index] != null)
            return catDeathList[index];
        else
        {
            Debug.Log("CatDeathList Dont have Index="+index);
            return null;
        }
    }

    public Transform GetPlayer()
    {
        if (player != null)
            return player;
        else
            return null;
    }
}
