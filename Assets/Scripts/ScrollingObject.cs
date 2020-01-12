using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {
   
    private   Rigidbody2D rb2d;

   
    // Use this for initialization
    void Start ()
    {
        FindObjectOfType<Bird>().OnSpeed += SpeedTime;
        FindObjectOfType<Bird>().OnStopScroling += OnStopScroling;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2 (GameControl.instance.scrollSpeed, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameControl.instance.gameOver == true)
        {
            rb2d.velocity = Vector2.zero;
        }
	}
    void SpeedTime(GameObject sprite)
    {
            StartCoroutine(Speed(sprite));
    }
    IEnumerator Speed(GameObject sprite)
    {
        
        rb2d.velocity = new Vector2(-12, 0);
        ColumnPool.instanceC.spawnRate = 1f;
        yield return new WaitForSeconds(2.5f);
        ColumnPool.instanceC.spawnRate = 4f;
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
        GameObject.FindObjectOfType<Bird>().isSpeed = false;
        sprite.SetActive(false);
        StopCoroutine("Speed");
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
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
    }
}
