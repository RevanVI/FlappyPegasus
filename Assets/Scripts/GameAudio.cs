using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    
    public AudioClip JumpAudio;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.clip = JumpAudio;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.Play();
        }
    }
}
