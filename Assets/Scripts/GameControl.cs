using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    public Text scoreText;
    public GameObject gameOverText;
    private Bird birdRef;
    public bool gameOver = false;
    public bool isSpeeded = false;
    public float scrollSpeed = -1.5f;
    private int score = 0;

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
	}
    private void Start()
    {
        //string str = GameObject.Find("SavePoint").gameObject.GetComponent<Text>().text;
        score = SavePoint.instance.score;
        scoreText.text = "Score: " + score;

        //score = int.Parse(str); //(int)char.GetNumericValue(str[str.Length-1]);
        birdRef = FindObjectOfType<Bird>();
    }
    void Update ()
    {
		//if (gameOver == true && Input.GetKeyDown(KeyCode.Space) &&)
  //      {
  //          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  //      }
	}

    public void BirdScored()
    {
        if (gameOver)
            return;
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        if (birdRef.isReLife)
        {
            birdRef.isReLife = false;
            SavePoint.instance.score = score;
            SceneManager.LoadScene("Game");
        }
        else
        {
            GameObject.FindObjectOfType<GameUi>().OnGameOver();
            gameOver = true;
        }
    }
}
 