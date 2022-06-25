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
    Vector3 plankPos;


    BoxCollider2D blade;

    GameObject plank;


    BoxCollider2D balloon;

    BoxCollider2D umbrella;


    void Start()
    {

        LevelEventManager.Instance.RegisterLevelEvnent(5, () =>
        {
            //刀片下来，砍断木板，打死猫，猫复活，猫踩木板过去
           GameObject go= new GameObject();

            Observable.Interval(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                blade.transform.position -= Vector3.up * 0.2f;
            }).AddTo(go);

            blade.OnCollisionEnter2DAsObservable()
            .Subscribe(_ => {

                plank.AddComponent<Rigidbody2D>();
                plank.AddComponent<BoxCollider2D>();
                blade.enabled = false;

                GameManager.Instance.ResetDead(5f, player, () =>
                {
                    player.transform.position = plankPos;

                },4);


            }).AddTo(blade);


        });

        LevelEventManager.Instance.RegisterLevelEvnent(6, () =>
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
                      
                    },5);
                    GameObject.Destroy(go);
                }
            }).AddTo(go);

          
        });

        LevelEventManager.Instance.RegisterLevelEvnent(7, () =>
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

              

            },6);



            LevelEventManager.Instance.RegisterLevelEvnent(8, () =>
            {
                //猫碰气球，气球上升，碰到雨伞，雨伞落下，猫死亡，猫复活，伞打开，鸽子飞出来，打开桥

               

                balloon.OnCollisionEnterAsObservable()
                .Subscribe(_ =>
                {
                    if (_.transform.tag=="Player")
                    {

                       GameObject go= new GameObject(); 

                        Observable.Interval(TimeSpan.FromSeconds(0.5f))
                        .Subscribe(t_ =>
                        {
                            balloon.transform.position += Vector3.up * 0.2f;
                        }).AddTo(go);
                    }

                    if (_.transform.tag == "Umbrella")
                    {

                        GameObject go = new GameObject();

                        Observable.Interval(TimeSpan.FromSeconds(0.5f))
                        .Subscribe(t =>
                        {
                            balloon.transform.position += Vector3.up * 0.2f;
                        }).AddTo(go);

                        umbrella.gameObject.AddComponent<Rigidbody2D>();
                    }

                }).AddTo(balloon);


                umbrella.OnCollisionEnter2DAsObservable()
                .Subscribe(_ =>
                {
                    GameManager.Instance.ResetDead(5f, player, () => { 
                    


                     },7);
                }).AddTo(umbrella);

            });

        });
    }

  
}
