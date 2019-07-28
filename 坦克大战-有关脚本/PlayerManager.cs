using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //属性值
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeated;


    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text playerlifeValueText;
    public GameObject isDefeatUI;
    //单例
    private static PlayerManager instance;
    public static PlayerManager Instance
    { get => instance;
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
        if(isDefeated)
        {
            isDefeatUI.active = true;
            Invoke("returntomain", 3);
            return;
        }


        if(isDead)
        {
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        playerlifeValueText.text=lifeValue.ToString();
    }

    private void Recover()
    {
        if(lifeValue<=0)
        {
            //游戏失败 返回主界面
            isDefeated = true;
            Invoke("returntomain", 3);

        }
        else
        {
            lifeValue--;
            GameObject player = Instantiate(born, new Vector3(-2, -10, 0), Quaternion.identity);
            player.GetComponent<Born>().createplayer = true;
            isDead = false;

        }
    }

    private void returntomain()
    {
        SceneManager.LoadScene(0);
    }
}
