using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {
   
    private   Rigidbody2D rb2d;
    // Use this for initialization
    void Start ()
    {
        FindObjectOfType<Bird>().OnSpeedActivate += SpeedActivate;
        FindObjectOfType<Bird>().OnSpeedDeactivate += SpeedDeactivate;
        //FindObjectOfType<Bird>().OnStopScroling += OnStopScroling;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (GameControl.instance.isSpeeded)
            rb2d.velocity = new Vector2(-12, 0);
        else
            rb2d.velocity = new Vector2 (GameControl.instance.scrollSpeed*2, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameControl.instance.gameOver == true)
        {
            rb2d.velocity = Vector2.zero;
        }
	}
    void SpeedActivate(int p)
    {
        if (this != null)
        {
            Debug.Log("Object speedup " + gameObject.name);
            rb2d.velocity = new Vector2(-12, 0); //speedup scrolling
        }
    }

    void SpeedDeactivate(int p)
    {
        if (this != null)
        {
            Debug.Log("Object stop speedup " + gameObject.name);
            rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed * 2, 0); //return scrolling speed to default
        }
    }

    public void OnStopScroling(int speed=0)
    {
        StartCoroutine(StopScrol());
    }
    IEnumerator StopScrol()
    {
        rb2d.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);
        StopCoroutine("StopScrol");
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed*2, 0);
    }
}
