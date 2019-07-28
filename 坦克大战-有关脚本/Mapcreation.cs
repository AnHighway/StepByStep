using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapcreation : MonoBehaviour
{   

    //用来装饰初始化地图所需物体的数组
    //0号老家，1号墙，2号障碍，3号出生效果，4号河流，5号草，6号空气墙
    public GameObject[] item;
    //已有东西的位置列表
    private List<Vector3> ItemPositionList = new List<Vector3>();



    private void Awake()
    {
        InitMap();

    }

    private void InitMap()
    {
        //实例化老家
        CreateItem(item[0], new Vector3(0, -10, 0), Quaternion.identity);
        //实例化老家周围的墙
        CreateItem(item[1], new Vector3(-1, -10, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -10, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -9, 0), Quaternion.identity);
        }
        //实例化地图其他物品
        for (int i = 0; i < 40; i++)
        {
            CreateItem(item[1], CreatRandomPositon(), Quaternion.identity);//墙
            CreateItem(item[2], CreatRandomPositon(), Quaternion.identity);//障碍
            CreateItem(item[4], CreatRandomPositon(), Quaternion.identity);//河流
            CreateItem(item[5], CreatRandomPositon(), Quaternion.identity);//草
        }


        //实例化空气墙
        for (int i = -13; i < 14; i++)
        {
            CreateItem(item[6], new Vector3(i, -11, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, 11, 0), Quaternion.identity);
        }
        for (int i = -10; i < 11; i++)
        {
            CreateItem(item[6], new Vector3(-14, i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(14, i, 0), Quaternion.identity);
        }
        //初始化玩家
        GameObject player = Instantiate(item[3], new Vector3(-2, -10, 0), Quaternion.identity);
        player.GetComponent<Born>().createplayer = true;

        //初始化敌人
        CreateItem(item[3], new Vector3(-13, 10, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 10, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(13, 10, 0), Quaternion.identity);

        //隔一段时间产生一个敌人
        InvokeRepeating("CreatEnemy", 4, 5);
    }
    private void CreateItem(GameObject creobject,Vector3 creposition,Quaternion crerotation)
    {
        GameObject itemOb = Instantiate(creobject, creposition, crerotation);
        itemOb.transform.SetParent(gameObject.transform);
        ItemPositionList.Add(creposition);
    }

    //产生随机位置的方法
    private Vector3 CreatRandomPositon()
    {
        //不生成X=+-12.5,Y不生成+-10这四个位置
        while(true)
        {
            Vector3 RandomPosition=new Vector3(Random.Range(-13,13),Random.Range(-8.5f,9.5f),0);
            if (!HasthePosition(RandomPosition))
            {
                return RandomPosition;
            }
            
        }
    }

    //判断位置列表中是否有重复位置
    private bool HasthePosition(Vector3 Randpos)
    {
        for(int i=0;i<ItemPositionList.Count;i++)
        {
            if (Randpos == ItemPositionList[i]) return true;
        }
        return false;
    }

    //产生敌人的方法
    private void CreatEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 Enemypos = new Vector3();
        if(num==0)
        {
            Enemypos = new Vector3(-13, 10, 0);
        }
        if (num == 1)
        {
            Enemypos = new Vector3(0, 10, 0);
        }
        if (num == 2)
        {
            Enemypos = new Vector3(13, 10, 0);
        }
        CreateItem(item[3], Enemypos, Quaternion.identity);
    }
}
