using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    //属性值
   
    public float cantmove;
    public bool isDefeated;


    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text playerlifeValueText;
    public GameObject isDefeatUI;
    //单例
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get => instance;
        set => instance = value;
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeated)
        {
            isDefeatUI.active = true;
            Invoke("returntomain", 3);
            return;
        }


        //if (isDead)
        //{
        //    Recover();
        //}
        //playerScoreText.text = playerScore.ToString();
        //playerlifeValueText.text = lifeValue.ToString();
    }

  

    private void returntomain()
    {
        SceneManager.LoadScene(0);
    }
}
