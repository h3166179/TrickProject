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

 

    Vector3 HatPos;
 





    

   


    #region level 5

    public SpriteRenderer plankRender;
    public BoxCollider2D bladeCollider;
    public BoxCollider2D bladeTrigger;

    public Sprite PlankspriteTop;
    public SpriteRenderer PlankspriteDown1;
    public SpriteRenderer PlankspriteDown2;
    public Vector3 plankPos;
    public GameObject placePlayer;
    #endregion
    public GameObject waterPlayer;
    public Vector3 waterPos;
    public BoxCollider2D waterTrriger;

    public BoxCollider2D powerTrigger;
    public GameObject powerPlayer;
    public Vector3 powerPos;

    public BoxCollider2D hairBoxCollider;
    public BoxCollider2D HatTrigger;

    public Vector3 endHatPos;

    public BoxCollider2D balloonTrigger;
    public GameObject balloonPlayer;


    public BoxCollider2D umbrella;

    public Vector3 umbrellaPos;

    public GameObject Openumbrella;

    public Sprite[] pigeonSprites;

    public SpriteRenderer pigeon;


    public BoxCollider2D bridgeTrigger;
    public Vector3 lastPos;
    public Vector3 startPigeonPos;
    public GameObject WaterDeadthTri;

    void Start()
    {
        PlayerController controller = player.GetComponent<PlayerController>();

        bladeTrigger.OnTriggerEnter2DAsObservable()
            .Where(_=>_.tag=="Player")
            .Subscribe(_ =>
            {
                LevelEventManager.Instance.LevelEventDic[5]();
                controller.isMove = false;
            }).AddTo(gameObject);


        LevelEventManager.Instance.RegisterLevelEvnent(5, () =>
        {
            //刀片下来，砍断木板，打死猫，猫复活，猫踩木板过去
            GameObject.Destroy(bladeTrigger.gameObject);
            plankRender.sprite = PlankspriteTop;
            PlankspriteDown1.gameObject.SetActive(true);
            PlankspriteDown2.gameObject.SetActive(true);
            // bladeCollider.gameObject.AddComponent<Rigidbody2D>();
            Observable.Interval(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                bladeCollider.transform.position -= Vector3.up ;
            }).AddTo(bladeCollider);

            bladeCollider.OnCollisionEnter2DAsObservable()
            .Where(_=>_.gameObject.tag=="Player")
            .Subscribe(_ => {

               
                controller.SetDead();
                DialogManager.Instance.DialogPlay(4);
                GameManager.Instance.HealthUIUpdate();
                GameManager.Instance.ResetDead(0f, player, () =>
                {
                    //player.transform.position = plankPos;
                    player.gameObject.SetActive(false);
                    placePlayer.gameObject.SetActive(true);

                    controller.renderer.flipY = false;
                    controller.isJump = true;
                    controller.isDead = false;
                    controller.isMove = true;
                    controller.renderer.sprite = controller.Idle;
                },4,false);


                GameObject.Destroy(bladeCollider.gameObject);

            }).AddTo(bladeCollider);


        });

        BoxCollider2D waterBoxCollider=waterBox.GetComponent<BoxCollider2D>();

        waterTrriger.OnTriggerEnter2DAsObservable()
            .Where(_=>_.tag=="Player")
            .Subscribe(_ =>
            {
          
                LevelEventManager.Instance.LevelEventDic[6]();
            }).AddTo(waterTrriger);


        LevelEventManager.Instance.RegisterLevelEvnent(6, () =>
        {
            //水箱
            waterBox.material.SetFloat("Hight",0.4f);
            waterTrriger.enabled = false;
            GameObject go = new GameObject();
            Observable.EveryUpdate()
            .Subscribe(_ =>
            {
              float h=  Mathf.Lerp(waterBox.material.GetFloat("Hight"), 1, Time.deltaTime*0.1f);
      
                waterBox.material.SetFloat("Hight",h);
              
             

                if (waterBox.material.GetFloat("Hight") >0.7f)
                {
                   

                    if (waterBoxCollider.isTrigger)
                    {
                        h = 1;
                        waterBoxCollider.isTrigger = false;
                      
                        waterBox.material.SetFloat("Hight", h);
                        DialogManager.Instance.DialogPlay(5);
                        GameManager.Instance.HealthUIUpdate();
                        GameManager.Instance.ResetDead(0f, player, () =>
                        {
                            player.gameObject.SetActive(false);
                            waterPlayer.gameObject.SetActive(true);
                            //player.transform.position = waterPos;
                            WaterDeadthTri.gameObject.SetActive(true);
                            controller.isDead = false;
                            controller.isMove = true;
                            controller.renderer.sprite = controller.Idle;
                            GameObject.Destroy(go);
                            //waterBox.GetComponent<BoxCollider2D>().enabled = true;
                            //waterBox.gameObject.AddComponent<Rigidbody2D>();

                        }, 5,false);
                    }
                 
                  
                }
               
            }).AddTo(go);

          
        });


        powerTrigger.OnTriggerEnter2DAsObservable()
            .Where(_=>_.tag=="Player")
            .Subscribe(_ =>
            {
                LevelEventManager.Instance.LevelEventDic[7]();
            }).AddTo(powerTrigger);

        hairBoxCollider.OnTriggerEnter2DAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Where(_ =>powerPlayer.activeSelf)
            .Subscribe(_ =>
            {

                controller.rigidbody2D.simulated = false;
                Observable.EveryUpdate()
                 .Subscribe(t =>
                 {
                     controller.transform.position += new Vector3(0, Time.deltaTime);
                    // controller.rigidbody2D.velocity = new Vector2(controller.rigidbody2D.velocity.x, 4f*Time.deltaTime);
                     if (Vector3.Distance(controller.transform.position,HatTrigger.transform.position)<=1)
                     {

                         player.transform.position = endHatPos;
                         controller.rigidbody2D.simulated=true;
                        // player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 4f);
                         controller.isJump = false;
                         controller.isState = false;
                         controller.isDead = false;
                         controller.isMove = true;
                         GameObject.Destroy(hairBoxCollider.gameObject);
                     }


                 }).AddTo(hairBoxCollider);

            }).AddTo(hairBoxCollider);


     

        LevelEventManager.Instance.RegisterLevelEvnent(7, () =>
        {

            controller.SetDead();
            GameObject.Destroy(powerTrigger.gameObject);
            DialogManager.Instance.DialogPlay(6);
            GameManager.Instance.HealthUIUpdate();
            GameManager.Instance.ResetDead(0f, player, () =>
            {
                Observable.Timer(TimeSpan.FromSeconds(2f))
                .Subscribe(_ =>
                {
                    player.transform.position = powerPos;
                    powerPlayer.gameObject.SetActive(true);

                    controller.renderer.flipY = false;
                    controller.isJump = true;
                    controller.isDead = false;
                    controller.isMove = true;
                    controller.renderer.sprite = controller.Idle;
                }).AddTo(gameObject);
               
               
            }, 6,false);


            balloonTrigger.OnTriggerEnter2DAsObservable()
              .Subscribe(_ =>
              {
          
                  if (_.transform.tag == "Player")
                  {

                    
                      Observable.Interval(TimeSpan.FromSeconds(0.5f))
                      .Subscribe(t_ =>
                      {
                          balloonTrigger.transform.position += Vector3.up * 0.2f;
                      }).AddTo(balloonTrigger);

                      Observable.Timer(TimeSpan.FromSeconds(2f))
                      .Subscribe(t =>
                      {
                          umbrella.gameObject.AddComponent<Rigidbody2D>();
                          GameObject.Destroy(balloonTrigger.gameObject);
                      }).AddTo(balloonTrigger);
                  }

                

              }).AddTo(balloonTrigger);


            umbrella.OnCollisionEnter2DAsObservable()
             .Where(_ => _.gameObject.tag == "Player")
            .Subscribe(_ =>
            {
                DialogManager.Instance.DialogPlay(7,true);
                GameManager.Instance.HealthUIUpdate();
                GameManager.Instance.ResetDead(0f, player, () =>
                {
                    player.transform.position = umbrellaPos;
                    balloonPlayer.gameObject.SetActive(true);

                    controller.renderer.flipY = false;
                    controller.isJump = true;
                    controller.isDead = false;
                    controller.isMove = true;
                    controller.renderer.sprite = controller.Idle;


                    Observable.Timer(TimeSpan.FromSeconds(3f))
                    .Subscribe(t =>
                    {
                        Openumbrella.gameObject.SetActive(true);
                        umbrella.gameObject.SetActive(false);
                        for (int i = 0; i <9; i++)
                        {
                            Observable.Timer(TimeSpan.FromSeconds(i))
                            .Subscribe(time =>
                            {
                                SetPigeon();
                            }).AddTo(gameObject);
                        }
                    }).AddTo(umbrella);

                }, 7,false);
            }).AddTo(umbrella);


          
                //猫碰气球，气球上升，碰到雨伞，雨伞落下，猫死亡，猫复活，伞打开，鸽子飞出来，打开桥


        });


       
    }

  
    
    public void SetPigeon()
    {
        int index=0;
        bool isEnd=false;
        bool isStart = false;
        bool isExit = false;

        GameObject go=GameObject.Instantiate(pigeon.gameObject);
        go.SetActive(true);
        SpriteRenderer renderer= go.GetComponent<SpriteRenderer>();
        Observable.Interval(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
               
                renderer.sprite = pigeonSprites[index];
                index++;
                if (index== pigeonSprites.Length)
                {
                    index = 0;
                }
            }).AddTo(go);


        Observable.EveryUpdate()
            .Where(_=>go.name!= "endPigeon")
            .Subscribe(_ =>
            {
              
               

                if (Vector3.Distance(go.transform.position, bridgeTrigger.transform.position) <= 1f)
                {
                    isEnd = true;
                }

                if (!isStart)
                {
                    if (Vector3.Distance(go.transform.position, startPigeonPos) <= 1)
                    {
                        go.transform.position += Vector3.right * Time.deltaTime*0.1f;
                        isStart = true;
                    }
                    else
                    {
                        go.transform.position = Vector3.Lerp(go.transform.position, startPigeonPos, Time.deltaTime);
                    }
                }


               
                if (isStart && !isEnd)
                {
                    go.transform.position = Vector3.Lerp(go.transform.position, bridgeTrigger.transform.position+Vector3.up, 0.05f);

                    if (Vector3.Distance(go.transform.position, bridgeTrigger.transform.position + Vector3.up) <= 1f)
                    {
                        isEnd = true;

                    }
                }



                if (isEnd&&!isExit)
                {
                    go.transform.position = Vector3.Lerp(go.transform.position, lastPos+Vector3.right*2, 0.02f);
                    if (Vector3.Distance(go.transform.position, lastPos+Vector3.right * 2) <= 1f)
                    {
                        go.transform.position = lastPos + Vector3.right * 2;
                        lastPos = go.transform.position;
                        isExit = true;
                    }
                }

              

            }).AddTo(go);
    }
}
