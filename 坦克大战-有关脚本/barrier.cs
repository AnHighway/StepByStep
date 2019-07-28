using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour
{
    public AudioClip hitaudio;
    // Start is called before the first frame update
  public void Playaudio()
    {
        AudioSource.PlayClipAtPoint(hitaudio, transform.position);
    }
}
