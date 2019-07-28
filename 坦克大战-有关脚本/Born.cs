using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{   public GameObject playerprefab;

    public GameObject[] enemyprefabs;

    public bool createplayer;
    // Start is called before the first frame update
    void Start()
    {
       
        
            Invoke("BornTank", 1);
            Destroy(gameObject, 1f);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void BornTank()
    { if (createplayer)
        { 
            Instantiate(playerprefab, transform.position, Quaternion.identity);
         }
    else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyprefabs[num], transform.position, Quaternion.identity);
        }
    }
}
