using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (SystemInfo.operatingSystem.Contains("Android"))
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
    }
    //перезапуск сцены
    public static void Game()
    {
        SceneManager.LoadScene("Game");
    }
    public static void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }
    // выход из игры
    public static void Exit()
    {
        Application.Quit();
    }

}
