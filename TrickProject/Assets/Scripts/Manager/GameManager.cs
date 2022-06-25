﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public class GameManager : SingletonBlank<GameManager>
{
    private Transform player;
    //猫生命条
    private Canvas CatHealth;
    private Text healText;
    private List<GameObject> catDeathList = new List<GameObject>();
    public int LevelIndex = 1;
    public List<Transform> loadPoint = new List<Transform>();
    private int healthNum = 9;
    private Canvas HealthUI;
    private Camera camera;

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

    public void RegisterHealthUI(Canvas canvas)
    {
        HealthUI = canvas;
    }

    public void RegisterCamera(Camera camera)
    {
        this.camera = camera;
    }

    public Camera GetCamera()
    {
        return camera;
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


    public void ResetDead(float delay,Transform trans,Action action,int levelIndex=-1,bool isLoad=true)
    {
        //死亡
        if (GameManager.Instance.GetPlayer().GetComponent<PlayerController>().isDead)
            return;

        trans.GetComponent<PlayerController>().isMove = false;
        //隐藏
        //trans.gameObject.SetActive(false);
        SceneFader sceneFader = GameObject.Instantiate(Resources.Load<GameObject>("UI/Prefab/SceneFaderCanvas")).GetComponent<SceneFader>();
        sceneFader.SetInCallback((_) => {
            //复活
            trans.gameObject.SetActive(true);
            if (isLoad)
            {
                GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/DeadPoint"));
                obj.transform.position = trans.position;
            }
            trans.GetComponent<SpriteRenderer>().flipY = false;
            if(levelIndex!=-1)
                trans.position = GameManager.Instance.loadPoint[levelIndex].position;
            trans.GetComponent<PlayerController>().isMove = true;
            sceneFader.CanvasFadeIn();
        });

        Observable.Timer(TimeSpan.FromSeconds(delay))
            .Subscribe(_ =>
            {
              
                sceneFader.CanvasFadeOut();
                action?.Invoke();
            }).AddTo(trans);


    }
    
    public void HealthUIUpdate()
    {
        int new_num = healthNum - 1;
        healthNum--;
        Sprite sprite = Resources.Load<Sprite>("Texture/Health/health"+new_num.ToString());
        Image image = HealthUI.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        image.sprite = sprite;
    }
}
