using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager:SingletonBlank<DataManager>
{
    Dictionary<int, ItemData> DataDic = new Dictionary<int, ItemData>();

    public void SaveData(ItemData data,Action action)
    {
        if (!DataDic.ContainsKey(data.id))
        {
            DataDic.Add(data.id, data);
        }
        else
        {
            DataDic[data.id] = data;
        }
       

        action?.Invoke();
    }

    public void LoadData(Transform trans,int id,Action action)
    {
        if (DataDic.ContainsKey(id))
        {
            trans.position = DataDic[id].Pos;
            trans.eulerAngles = DataDic[id].Rot;
            trans.localScale = DataDic[id].Scale;
            action?.Invoke();
        }
    }
}

public class ItemData
{
   public  int id;
   public Vector3 Pos;
   public Vector3 Rot;
   public Vector3 Scale;
}

