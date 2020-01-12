using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float upForce = 200f;

    public GameObject ParticleSystem;

    private bool isDead = false;
    private float maxHeight = 4.6f;
    public bool isProtection = false;
    public bool isSpeed = false;
    public bool isReLife = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public AudioSource aud;

    public event System.Action<GameObject> OnSpeed;
    public event System.Action<int> OnStopScroling;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead == false)
        {
            
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))&& isSpeed)
            {
               
                gameObject.transform.Find("Speed").gameObject.SetActive(true);
                OnSpeed(gameObject.transform.Find("Speed").gameObject);
               
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("FlyUp");
                aud.Play();
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
            }
            if (transform.position.y >= maxHeight)
            {
                transform.position = new Vector3(-5f, maxHeight, 0f);
            }

        }
    }
    public void OnBonus(string isBonus)
    {
        if (isBonus == "Protection")
        {
            isProtection = true;
            gameObject.transform.Find("Protection").gameObject.SetActive(true);
        }
        if (isBonus == "Speed")
        {
            isSpeed = true;
            gameObject.transform.Find("Fleece").gameObject.SetActive(true);
        }
        if (isBonus == "ReLife")
        {
            isReLife = true;
            gameObject.transform.Find("Heart").gameObject.SetActive(true);
        }

    }



    void OnCollisionEnter2D(Collision2D coll)
    {
      
        if (isProtection)
        {
            isProtection = false;
            if (gameObject.transform.Find("Protection").gameObject)
                gameObject.transform.Find("Protection").gameObject.SetActive(false);

        }
        else if (isSpeed && coll.gameObject.tag == "Colum")
        {
 
        }
        else if (isReLife)
        {
            rb2d.velocity = Vector2.zero;
            StartCoroutine(ReLife(coll.transform.position));
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            isDead = true;
            aud.mute = true;
            anim.SetBool("isDead", true);
            GameControl.instance.BirdDied();
        }
    }
    IEnumerator ReLife(Vector2 posit)
    {
        gameObject.transform.Find("Heart").gameObject.SetActive(false);
        upForce = 0;
        OnStopScroling(0);
        yield return new WaitForSeconds(1f);
        isReLife = false;
        upForce = 200f;
        StopCoroutine("ReLife");
        gameObject.transform.position = new Vector2(posit.x + 1, posit.y + 2);
    }
}