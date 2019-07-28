using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   //属性值
    public float MoveSpeed = 3;
    private Vector3 BulletEularangles;
    private float CdTime;
    private float defendTime=3;
    private bool isdefended=true;
    //引用
    private SpriteRenderer Sr;
    public Sprite[] TankSprite;//上 右 下 左
    public GameObject bulletprefab;
    public GameObject explosionprefab;
    public GameObject defendeffectprefab;
    public AudioSource moveaudio;
    public AudioClip[] tankaudio;


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
        //是否处于无敌状态
        if(isdefended)
        {
            defendeffectprefab.SetActive(true);
            defendTime -= Time.deltaTime;
            if(defendTime<=0)
            {
                isdefended = false;
                defendeffectprefab.SetActive(false);
            }
        }

        //攻击的CD
        
        
    }
    private void FixedUpdate()
    {
        if(PlayerManager.Instance.isDefeated==true)//老家没了玩家不能再移动了
        {
            return;
        }
        move();
        if (CdTime>=0.4f)
        {
            attack();
        }
        else
        {
            CdTime += Time.fixedDeltaTime;
        }


    }
    //移动
    private void move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector3.right * h * MoveSpeed * Time.fixedDeltaTime, Space.World);

        if (h < 0)
        {
            Sr.sprite = TankSprite[3];
            BulletEularangles = new Vector3(0, 0, 90);
        }

        else if (h > 0)
        {
            Sr.sprite = TankSprite[1];
            BulletEularangles = new Vector3(0, 0, -90);
        }
        if(Mathf.Abs(h)>0.5f)
        {
            moveaudio.clip = tankaudio[1];
            
            if(!moveaudio.isPlaying)
            {
                moveaudio.Play();
            }
        }
        else
        {
            moveaudio.clip = tankaudio[0];

            if (!moveaudio.isPlaying)
            {
                moveaudio.Play();
            }
        }
        


        if (h != 0) return;

        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.up * v * MoveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0)
        {
            Sr.sprite = TankSprite[2];
            BulletEularangles = new Vector3(0, 0, -180);
        }

        else if (v > 0)
        {
            Sr.sprite = TankSprite[0];
            BulletEularangles = new Vector3(0, 0, 0);
        }
        if (Mathf.Abs(v) > 0.5f)
        {
            moveaudio.clip = tankaudio[1];

            if (!moveaudio.isPlaying)
            {
                moveaudio.Play();
            }
            else
            {
                moveaudio.clip = tankaudio[0];

                if (!moveaudio.isPlaying)
                {
                    moveaudio.Play();
                }
            }

        }

    }
    //攻击
    private void attack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //子弹产生的角度应该是当前坦克的角度+子弹应该旋转的角度
            Instantiate(bulletprefab, transform.position, Quaternion.Euler(transform.eulerAngles+BulletEularangles));//欧拉角转四元数
            CdTime = 0;

        }
    }
    //坦克死亡
    private void die()
    {
        if(isdefended)
        { return; }
        PlayerManager.Instance.isDead = true;
        //产生爆炸特效
        Instantiate(explosionprefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Addlife":
                PlayerManager.Instance.lifeValue++;
                break;

            case "TimemPause":
                EnemyManager.Instance.cantmove = 3;
                break;
        }
    }
}
