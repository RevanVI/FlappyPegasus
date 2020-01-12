using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public GameObject GameOver;
    public Image fadePlane;
    public AudioClip defeatSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnGameOver()
    {
        GameOver.SetActive(true);
        AudioSource aSource = GameObject.Find("Music").GetComponent<AudioSource>();
        aSource.clip = defeatSound;
        aSource.volume = 0.5f;
        aSource.Play();
        StartCoroutine(Fade(Color.clear, Color.black, 1));
    }
    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;
        while (percent < 0.85)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
        StopCoroutine("Fade");
    }
    public void Game()
    {
        SceneManager.LoadScene("Game");
    }
    public void StartMenu()
    {
        SceneManager.LoadScene("Start");
    }
}
