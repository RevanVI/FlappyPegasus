using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLandscape : MonoBehaviour
{
    private Rigidbody2D rb2d;
    //private float colliderLength;
    // Start is called before the first frame update
    void Start()
    {
        //colliderLength = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver == true)
            rb2d.velocity = Vector2.zero;
        if (transform.position.x <= -22.65)
            transform.position = new Vector3(22.65f, transform.position.y, 0f);
    }
}
