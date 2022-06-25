using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager:SingletonBlank<DataManager>
{
    Dictionary<string, Transform> itemDic = new Dictionary<string, Transform>();
    Dictionary<string, ItemData> DataDic = new Dictionary<string, ItemData>();

    public void SaveData(Transform trs,Action action,Sprite sprite=null)
    {
        ItemData itemData = new ItemData(trs.GetComponent<InteractiveItem>().id,
            trs.position,trs.eulerAngles,trs.localScale, sprite);
        if (!DataDic.ContainsKey(itemData.id))
        {
            DataDic.Add(itemData.id, itemData);
            itemDic.Add(itemData.id, trs);
        }

        action?.Invoke();
    }

    public void LoadData(string id,Action action)
    {
        Transform trans = itemDic[id];
        if (trans!=null && DataDic.ContainsKey(id))
        {
            trans.position = DataDic[id].Pos;
            trans.eulerAngles = DataDic[id].Rot;
            trans.localScale = DataDic[id].Scale;
            if (trans.GetComponent<SpriteRenderer>() != null &&
                trans.GetComponent<SpriteRenderer>().sprite != DataDic[id].sprite)
                trans.GetComponent<SpriteRenderer>().sprite = DataDic[id].sprite;
            trans.gameObject.SetActive(true);
            action?.Invoke();
        }
    }

    public void LoadLevelData(int sceneId)
    {
        switch (sceneId)
        {
            case 1:
                LoadData("1",null);
                LoadData("2", null);
                break;
        }

    }



}

public class ItemData
{
    public string id;
    public Vector3 Pos;
    public Vector3 Rot;
    public Vector3 Scale;
    public Sprite sprite;

    public ItemData()
    {

    }
  
    public ItemData(string id, Vector3 pos, Vector3 rot, Vector3 scale,Sprite sprite)
    {
        this.id = id;
        Pos = pos;
        Rot = rot;
        Scale = scale;
        this.sprite = sprite;
    }
}

