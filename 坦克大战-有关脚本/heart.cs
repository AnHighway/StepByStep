using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heart : MonoBehaviour
{
    private SpriteRenderer sp;
    public Sprite brokeflag;
    public GameObject explosionprefab;
    public AudioClip dieAudio;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void die()
    {
        sp.sprite = brokeflag;
        Instantiate(explosionprefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeated = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
        
    }

   
}
