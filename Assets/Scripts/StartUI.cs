using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public AudioClip scrollSound;
    private GameObject mainMenu;
    private GameObject rulesMenu;
    private GameObject recordsMenu;
    private GameObject curMenu;
    private AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("Main");
        if (mainMenu == null)
            print("ref mainMenu is null");
        rulesMenu = GameObject.Find("RulesMenu");
        rulesMenu.SetActive(false);
        if (rulesMenu == null)
            print("ref rulesMenu is null");
        //recordsMenu = GameObject.Find("RecordsMenuMenu");
        curMenu = mainMenu;
        soundSource = GameObject.Find("SoundsSource").GetComponent<AudioSource>();
        if (soundSource == null)
            print("ref soundSource is null");
    }
    //перезапуск сцены
    public void Game()
    {
        soundSource.clip = scrollSound;
        soundSource.Play();
        SceneManager.LoadScene("Game");
    }
    public void Record()
    {
        soundSource.clip = scrollSound;
        soundSource.Play();
        SceneManager.LoadScene("Record");
    }
    // выход из игры
    public void Exit()
    {
        soundSource.clip = scrollSound;
        soundSource.Play();
        Application.Quit();
    }
    //открытие правил
    public void Rules()
    {
        mainMenu.SetActive(false);
        curMenu = rulesMenu;
        soundSource.clip = scrollSound;
        soundSource.Play();
        curMenu.SetActive(true);
    }
    //Возврат в главное меню
    public void ReturnMain()
    {
        curMenu.SetActive(false);
        curMenu = mainMenu;
        soundSource.clip = scrollSound;
        soundSource.Play();
        curMenu.SetActive(true);
    }


}
