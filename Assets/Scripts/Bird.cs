using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{

    public float upForce = 200f;

    private bool isDead = false;
    private float maxHeight = 4.6f;
    public bool isProtection = false;
    public bool isSpeed = false;
    public bool isReLife = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public AudioSource aud;


    public event System.Action<int> OnSpeedActivate;
    public event System.Action<int> OnSpeedDeactivate;
    //public event System.Action<int> OnStopScroling;


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
            //Controls for PC
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("FlyUp");
                rb2d.AddForce(Vector2.up * (upForce - rb2d.velocity.y), ForceMode2D.Impulse);
            }
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && isSpeed && !GameControl.instance.isSpeeded)
            {
                StartCoroutine(SpeedUpActivate());
            }
            //Controls for mobile
            if (Input.touches.Length > 0)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    anim.SetTrigger("FlyUp");
                    rb2d.AddForce(Vector2.up * (upForce - rb2d.velocity.y), ForceMode2D.Impulse);
                }
                if (touch.phase == TouchPhase.Moved && isSpeed && !GameControl.instance.isSpeeded)
                {
                    StartCoroutine(SpeedUpActivate());
                }
            }


            if (transform.position.y >= maxHeight)
            {
                transform.position = new Vector3(-7f, maxHeight, 0f);
            }
            if (transform.position.y <= -5f)
            {
                rb2d.velocity = Vector2.zero;
                isDead = true;
                aud.mute = true;
                anim.SetBool("isDead", true);
                GameControl.instance.BirdDied();
            }
        }
    }

    public void OnBonus(string isBonus)
    {
        if (isBonus == "Protection")
        {
            anim.SetBool("isProtected", true);
            isProtection = true;
            gameObject.transform.Find("Protection").gameObject.SetActive(true);
        }
        else if (isBonus == "Speed")
        {
            isSpeed = true;
            gameObject.transform.Find("Fleece").gameObject.SetActive(true);
        }
        else if (isBonus == "ReLife")
        {
            isReLife = true;
            gameObject.transform.Find("Heart").gameObject.SetActive(true);
        }

    }

    IEnumerator SpeedUpActivate()
    {
        GameControl.instance.isSpeeded = true;
        gameObject.transform.Find("Fleece").gameObject.SetActive(false);
        isSpeed = false;
        anim.SetBool("Speeded", true);
        gameObject.transform.Find("Speed").gameObject.SetActive(true);
        ColumnPool.instanceC.SpeedActivate(); //disable collisions with columns
        OnSpeedActivate(0);
        yield return new WaitForSeconds(2.5f); //for 2.5 seconds
        Debug.Log("SpeedUpCoroutine deactivate");
        OnSpeedDeactivate(0);
        ColumnPool.instanceC.SpeedDeactivate();
        anim.SetBool("Speeded", false);
        GameControl.instance.isSpeeded = false;
        StopCoroutine("SpeedUpActivate");
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!isDead) {
            if (isProtection)
            {
                isProtection = false;
                anim.SetBool("isProtected", false);
                if (gameObject.transform.Find("Protection").gameObject)
                    gameObject.transform.Find("Protection").gameObject.SetActive(false);

            }
            else if (isSpeed && coll.gameObject.tag == "Colum")
            {

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
    }
    
}