using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{

    public int levelNum;


    private void Start()
    {
        if(levelNum != 0)
        {
            PlayerPrefs.SetInt("LastLevel", levelNum);
        }
    }

    public void loadManiMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void loadLevelSelection()
    {
        SceneManager.LoadScene(1);
    }

    public void loadInfo()
    {
        SceneManager.LoadScene(1);
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt("LastLevel", 2);
    }


    public void loadLevel2()
    {
        SceneManager.LoadScene(3);
        PlayerPrefs.SetInt("LastLevel", 3);
    }


    public void loadLevel3()
    {
        SceneManager.LoadScene(4);
        PlayerPrefs.SetInt("LastLevel", 4);
    }

    public void loadLastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel", 2));
    }


    public void loadNextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel", 2) + 1);
    }

    public void loadEndScreen()
    {
        SceneManager.LoadScene(5);
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
