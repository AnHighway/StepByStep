using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //属性值
    public float MoveSpeed = 3;
    private Vector3 BulletEularangles;
    private Vector3 EnemyPosition;

    private float vertical=-1;
    private float horizotal;
   
  
    //引用
    private SpriteRenderer Sr;
    public Sprite[] TankSprite;//上 右 下 左
    public GameObject bulletprefab;
    public GameObject explosionprefab;
   
    //计时
    private float CdTime;
    private float ChangeDirection;

    private void Awake()
    {
        Sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         EnemyPosition = transform.position;

        //攻击的CD
        if (CdTime >= 0.7f)
        {
            attack();
        }
        else
        {
            CdTime += Time.deltaTime;
        }


    }
    private void FixedUpdate()
    {

        //if (EnemyManager.Instance.cantmove > 0)
        //{
        //    EnemyManager.Instance.cantmove--;
        //    return;
        //}
        /*else*/ move();
        

    }
    //移动
    private void move()
    {
       
        if (ChangeDirection>=2)
        {
            int num = Random.Range(0, 8);
            if(num>5)
            {
                vertical = -1;
                horizotal = 0;

            }
            else if(num==0)
            {
                vertical = 1;
                horizotal = 0;
            }
            else if(num>0&&num<=2)
            {
                horizotal = -1;
                vertical = 0;
            }
            else if(num>2&&num<=4)
            {
                horizotal = 1;
                vertical = 0;
            }
            ChangeDirection = 0;
        }
        else
        {
            ChangeDirection += Time.deltaTime;
        }


       
        transform.Translate(Vector3.right * horizotal * MoveSpeed * Time.fixedDeltaTime, Space.World);

        if (horizotal < 0)
        {
            Sr.sprite = TankSprite[3];
            BulletEularangles = new Vector3(0, 0, 90);
        }

        else if (horizotal > 0)
        {
            Sr.sprite = TankSprite[1];
            BulletEularangles = new Vector3(0, 0, -90);
        }

        if (horizotal != 0) return;

      

        transform.Translate(Vector3.up * vertical * MoveSpeed * Time.fixedDeltaTime, Space.World);

        if (vertical< 0)
        {
            Sr.sprite = TankSprite[2];
            BulletEularangles = new Vector3(0, 0, -180);
        }

        else if (vertical > 0)
        {
            Sr.sprite = TankSprite[0];
            BulletEularangles = new Vector3(0, 0, 0);
        }


    }
    //攻击
    private void attack()
    {
        
            //子弹产生的角度应该是当前坦克的角度+子弹应该旋转的角度
           GameObject itemOb = Instantiate(bulletprefab, transform.position, Quaternion.Euler(transform.eulerAngles + BulletEularangles));

            itemOb.transform.SetParent(gameObject.transform);
        CdTime = 0;
       
            
        


    }
    //坦克死亡
    private void die()
    {
        PlayerManager.Instance.playerScore++;
        //产生爆炸特效
        Instantiate(explosionprefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
    //两个坦克碰撞就进行转向
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="enemy")
        {
            ChangeDirection = 2;
        }
    }
}
