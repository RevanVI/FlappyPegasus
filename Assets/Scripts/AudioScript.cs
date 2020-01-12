using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip AudioClip;
    public AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.clip = AudioClip;
        AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
