using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1;
    public void wrr()
    {
        src.clip = sfx1;
        src.Play();
    }
}
