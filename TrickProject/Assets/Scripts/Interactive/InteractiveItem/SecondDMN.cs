using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDMN : InteractiveItem
{
    [SerializeField]private Transform openSwitch;

    protected override void EnterDelegate()
    {
        //TODO:播放逐帧
        //TODO:延迟开关(多米诺逐帧时间)
        Invoke("OpenSphere",2f);
    }

    //开启球开关
    private void OpenSphere()
    {
        //TODO:旋转开关
    }
}
