using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public GameObject GameOver;
    public Image fadePlane;
    public InputField field;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SaveRecord();
        }
    }
    public void OnGameOver()
    {
        GameOver.SetActive(true);
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
        SaveRecord();
        SceneManager.LoadScene("Game");
    }
    public void StartMenu()
    {
        SaveRecord();
        SceneManager.LoadScene("Start");
    }
    void SaveRecord()
    {
        //form user name
        string nameStr = GameObject.Find("NameText").gameObject.GetComponent<Text>().text;
        string scoreStr = GameObject.Find("ScoreText").gameObject.GetComponent<Text>().text.Split(' ')[1];
        int len = 0;
        bool charCheck = true;
        while (len < 15 && charCheck && len < nameStr.Length)
        {
            if (nameStr[len].Equals(","))
            {
                charCheck = false;
            }
            else
                ++len;
        }
        if (len != 0)
        {
            string[] newRecordItem = new string[2] { nameStr.Substring(0, len), scoreStr };
            List<string[]> data = DataHandler.readData();
            DataHandler.insertNewItem(ref data, newRecordItem);
            DataHandler.writeData(ref data);
        }
        SavePoint.instance.score = 0;
    }

}
