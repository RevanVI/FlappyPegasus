using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRuins : MonoBehaviour
{
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver == true)
        {
            rb2d.velocity = Vector2.zero;
        }
        if (transform.position.x <= -10f)
        {
            transform.position = new Vector3(-15f, -2f, 0f);
        }
    }
}
