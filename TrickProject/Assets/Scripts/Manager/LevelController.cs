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


    public BoxCollider2D powerTrigger;
    public GameObject powerPlayer;
    public Vector3 powerPos;

    public BoxCollider2D hairBoxCollider;
    public BoxCollider2D HatTrigger;

    public Vector3 endHatPos;

    public BoxCollider2D balloonTrigger;
    public GameObject balloonPlayer;


    public BoxCollider2D umbrella;

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

                GameManager.Instance.ResetDead(0f, player, () =>
                {
                    player.transform.position = plankPos;
                    placePlayer.gameObject.SetActive(true);

                    controller.renderer.flipY = false;
                    controller.isJump = true;
                    controller.isDead = false;
                    controller.isMove = true;
                    controller.renderer.sprite = controller.Idle;
                },4);


                GameObject.Destroy(bladeCollider.gameObject);

            }).AddTo(bladeCollider);


        });

        BoxCollider2D waterBoxCollider=waterBox.GetComponent<BoxCollider2D>();

        waterBoxCollider.OnTriggerEnter2DAsObservable()
            .Where(_=>_.tag=="Player")
            .Subscribe(_ =>
            {
          
                LevelEventManager.Instance.LevelEventDic[6]();
            }).AddTo(waterBoxCollider);


        LevelEventManager.Instance.RegisterLevelEvnent(6, () =>
        {
            //水箱
            waterBox.material.SetFloat("Hight",-0.5f);

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
                        GameManager.Instance.ResetDead(0f, player, () =>
                        {
                            waterPlayer.gameObject.SetActive(true);
                            player.transform.position = waterPos;
                     
                            controller.isDead = false;
                            controller.isMove = true;
                            controller.renderer.sprite = controller.Idle;
                            GameObject.Destroy(go);
                            //waterBox.GetComponent<BoxCollider2D>().enabled = true;
                            //waterBox.gameObject.AddComponent<Rigidbody2D>();

                        }, 5);
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

        hairBoxCollider.OnCollisionEnter2DAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Where(_ =>powerPlayer.activeSelf)
            .Subscribe(_ =>
            {
              
                controller.rigidbody2D.simulated = false;
                Observable.EveryUpdate()
                 .Subscribe(t =>
                 {
                     controller.transform.position+= new Vector3(0, Time.deltaTime);
                     if (Vector3.Distance(controller.transform.position,HatTrigger.transform.position)<=1)
                     {
                         GameObject.Destroy(hairBoxCollider);
                         player.transform.position = endHatPos;
                         controller.rigidbody2D.simulated = true;
                         controller.isState = true;
                     }
                 }).AddTo(hairBoxCollider);

            }).AddTo(hairBoxCollider);


     

        LevelEventManager.Instance.RegisterLevelEvnent(7, () =>
        {

            controller.SetDead();
            GameObject.Destroy(powerTrigger.gameObject);
            GameManager.Instance.ResetDead(0f, player, () =>
            {
                player.transform.position = powerPos;
                powerPlayer.gameObject.SetActive(true);

                controller.renderer.flipY = false;
                controller.isJump = true;
                controller.isDead = false;
                controller.isMove = true;
                controller.renderer.sprite = controller.Idle;
               
            }, 6);


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
                GameManager.Instance.ResetDead(0f, player, () =>
                {

                    player.transform.position = endHatPos;
                    balloonPlayer.gameObject.SetActive(true);

                    controller.renderer.flipY = false;
                    controller.isJump = true;
                    controller.isDead = false;
                    controller.isMove = true;
                    controller.renderer.sprite = controller.Idle;

                }, 7);
            }).AddTo(umbrella);


          
                //猫碰气球，气球上升，碰到雨伞，雨伞落下，猫死亡，猫复活，伞打开，鸽子飞出来，打开桥


        });
    }

  
    

}
