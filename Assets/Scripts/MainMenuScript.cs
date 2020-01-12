using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuScript : MonoBehaviour
{
    private GameObject[] menus;
    private int curMenu = 0;
    public AudioSource menuSounds;

    // Start is called before the first frame update
    void Start()
    {
        menus = new GameObject[3];
        menus[0] = GameObject.Find("MainMenu");
        menus[1] = GameObject.Find("RulesMenu");
        menus[2] = GameObject.Find("RecordsMenu");
        menus[1].SetActive(false);
        menus[2].SetActive(false);
    }

    public void PerformAction(int action)
    {
        menuSounds.Play();
        if (action < 3)
        {
            menus[curMenu].SetActive(false);
            menus[action].SetActive(true);
            curMenu = action;
        }
        else if (action == 3)
            StartUI.Game();
        else
            StartUI.Exit();
    }

    void ShowRecords()
    {
        menus[curMenu].SetActive(false);
        menuSounds.Play();
        menus[2].SetActive(true);
        curMenu = 2;
    }

    void ShowRules()
    {
        menus[curMenu].SetActive(false);
        menuSounds.Play();
        menus[1].SetActive(true);
        curMenu = 1;
    }

    void ShowMainMenu()
    {
        menus[curMenu].SetActive(false);
        menuSounds.Play();
        menus[0].SetActive(true);
        curMenu = 0;
    }
}
