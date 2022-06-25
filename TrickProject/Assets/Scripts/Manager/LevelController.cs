using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class LevelController : MonoBehaviour
{
    public SpriteRenderer waterBox;
    public Transform player;

    public Transform Hat;

    Vector3 HatPos;

    void Start()
    {
        LevelEventManager.Instance.RegisterLevelEvnent(5, () =>
        {
            //水箱
            waterBox.material.SetFloat("Hight",-0.5f);

            GameObject go = new GameObject();
            Observable.EveryUpdate()
            .Subscribe(_ =>
            {
              float h=  Mathf.Lerp(waterBox.material.GetFloat("Hight"), 1, 0.02f);
                waterBox.material.SetFloat("Hight",h);


                if (waterBox.material.GetFloat("Hight") >0.5f)
                {
                    h = 1;
                    waterBox.material.SetFloat("Hight", h);
                    GameManager.Instance.ResetDead(5f, player,()=> {

                        //waterBox.GetComponent<BoxCollider2D>().enabled = true;
                        //waterBox.gameObject.AddComponent<Rigidbody2D>();
                    
                    });
                    GameObject.Destroy(go);
                }
            }).AddTo(go);

          
        });

        LevelEventManager.Instance.RegisterLevelEvnent(6, () =>
        {
            GameManager.Instance.ResetDead(5f, player, () =>
            {

                //waterBox.GetComponent<BoxCollider2D>().enabled = true;
                //waterBox.gameObject.AddComponent<Rigidbody2D>();

                Hat.GetComponent<BoxCollider2D>().OnCollisionEnter2DAsObservable()
                .Subscribe(_ =>
                {
                    player.transform.SetParent(Hat);

                    Observable.Timer(TimeSpan.FromSeconds(2f))
                    .Subscribe(time =>
                    {
                        Hat.transform.position = HatPos;
                    }).AddTo(gameObject);
               

                }).AddTo(Hat.gameObject);

              

            });


        });
    }

  
}
