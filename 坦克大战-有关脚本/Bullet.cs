using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   public float MoveSpeed=10;
    public bool IsPlayerBullet=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * MoveSpeed * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
     switch(collision.tag)
        {
            case "tank":
                if (!IsPlayerBullet)
                { 
                    collision.SendMessage("die");
                }
                break;
            case "heart":
                collision.SendMessage("die");
                Destroy(gameObject);
                break;
            case "enemy":
                if (IsPlayerBullet)
                {
                    collision.SendMessage("die");
                    Destroy(gameObject);
                }
                
                break;
            case "wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "barrier":
                if (IsPlayerBullet)
                {
                    collision.SendMessage("Playaudio");
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
