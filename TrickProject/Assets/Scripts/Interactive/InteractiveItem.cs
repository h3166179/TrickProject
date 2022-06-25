using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : InteractiveBase
{
    public string id;
    [SerializeField] bool isSprite = false;

    protected override void Start()
    {
        base.Start();
        if (isSprite)
            DataManager.Instance.SaveData(this.transform, null, GetComponent<SpriteRenderer>().sprite);
        else
            DataManager.Instance.SaveData(this.transform,null);
    }
}
