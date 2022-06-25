using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LevelEventManager:SingletonBlank<LevelEventManager>
{
   public Dictionary<int, Action> LevelEventDic=new Dictionary<int, Action>();


   public void RegisterLevelEvnent(int levelIndex,Action action)
    {
        if (!LevelEventDic.ContainsKey(levelIndex))
        {
            LevelEventDic.Add(levelIndex, action);
        }
    }
}
