using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator anim;

    public float height;

    public float Speed;

    public Sprite[] walkImgList;
    public Sprite[] jumpImgList;

    SpriteRenderer renderer;
   public int walkIndex;
   public int jumpIndex;

    float h;

    BoxCollider2D collider2D;

    Vector3 lastCenter;

   public bool isJump;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        renderer = GetComponent<SpriteRenderer>();


        collider2D = GetComponent<BoxCollider2D>();

        GameManager.Instance.RegisterPlayer(this.transform);

        Observable.Interval(System.TimeSpan.FromSeconds(0.2f))
            .Subscribe(_ => {

                if (isJump)
                {
                    PlayMoveAnim(h < 0, jumpImgList, ref jumpIndex);
                }else
                {
                    if (h!=0)
                    {
                        PlayMoveAnim(h < 0, walkImgList,ref walkIndex);
                    }
           
                }
               
            }).AddTo(gameObject);


        collider2D.OnCollisionStay2DAsObservable()
                        .Where(_ => _.gameObject.tag == "Map")
               .Subscribe(_ =>
               {
                   isJump = true;  
            }).AddTo(gameObject);

        collider2D.OnCollisionExit2DAsObservable()
            .Where(_=>_.gameObject.tag=="Map")
            .Subscribe(_ =>
            {
                isJump = false;
            }).AddTo(gameObject);
    }


    void Update()
    {
         h = Input.GetAxis("Horizontal");
        float speed;

        if (Input.GetKeyDown(KeyCode.Space) && isJump)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, height);
     
        }

        
      
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            DataManager.Instance.LoadLevelData(GameManager.Instance.LevelIndex);
        }

      
       

        rigidbody2D.velocity = new Vector2(h*Speed, rigidbody2D.velocity.y);

    
      
    }

    void PlayMoveAnim(bool filp,Sprite[] imgList,ref int index)
    {
        if (renderer.flipX != filp)
        {
            renderer.flipX = filp;

        }
        else
        {
            renderer.sprite = imgList[index];
            index++;
            if (index == imgList.Length)
            {
                index = 0;
            }
        }

    }
}
