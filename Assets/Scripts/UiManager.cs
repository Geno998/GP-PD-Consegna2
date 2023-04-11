using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Security.Cryptography;

public class UiManager : MonoBehaviour
{

    public enum CurrentMenu
    {
        Main,
        Game,
        Levels,
        End
    }

    [SerializeField] CurrentMenu uI;
    [SerializeField] StageValuesScriptable values;

    [SerializeField] TMP_Text killCounter;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text winText;
    [SerializeField] TMP_Text pointsText;


    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject retryButton;


    [SerializeField] Button level2Button;
    [SerializeField] Button level3Button;

    int Score;
    public int points;
    int timePoints;
    int complitionPoints;



    public int enemiesKilled;
    int totalEnemies;





    public float minutes = 3;
    public float seconds;
    public float globalTime;




    sceneManager sM;



    private void Start()
    {
        if (uI == CurrentMenu.Game)
        {
            sM = GameManager.Instance.SM;
        }
    }

    private void Update()
    {
        if (uI == CurrentMenu.Game)
        {
            timerFlow();
            displayTime();
            enemiesLeft();
            displayPoints();
            winGame();
        }

        if (uI == CurrentMenu.End)
        {
            EndGameStatus();
        }

        if (uI == CurrentMenu.Levels)
        {
            if (PlayerPrefs.GetInt("UnlockedLevels", 0) == 0)
            {
                level2Button.interactable = false;
                level3Button.interactable = false;
            }
            else if (PlayerPrefs.GetInt("UnlockedLevels", 0) == 1)
            {
                level2Button.interactable = true;
                level3Button.interactable = false;
            }
            else if (PlayerPrefs.GetInt("UnlockedLevels", 0) == 2)
            {
                level2Button.interactable = true;
                level3Button.interactable = true;
            }
        }
    }


    void timerFlow()
    {
        globalTime += Time.deltaTime;


        if (seconds < 60.4f)
        {
            seconds += Time.deltaTime;
        }
        else
        {
            minutes++;
            seconds = -0.5f;
        }


    }

    void displayTime()
    {
        if (timeText != null)
        {
            if (minutes >= 0)
            {
                if (seconds >= 9.5)
                {
                    timeText.text = "TIME: 0" + minutes + ":" + Mathf.RoundToInt(seconds);
                }
                else
                {
                    timeText.text = "TIME: 0" + minutes + ":" + "0" + Mathf.RoundToInt(seconds);
                }
            }
        }
    }


    void enemiesLeft()
    {
        totalEnemies = values.enemy1ToSpawn + values.enemy2ToSpawn + values.enemy3ToSpawn;
        killCounter.text = enemiesKilled + " / " + totalEnemies;
    }

    void displayPoints()
    {
        pointsText.text = "POINTS: " + points;
    }

    void winGame()
    {
        if (enemiesKilled == totalEnemies)
        {
            sM.loadEndScreen();
            PlayerPrefs.SetInt("win", 1);


            if (sM.levelNum == 2 && PlayerPrefs.GetInt("UnlockedLevels", 0) < 1)
            {
                PlayerPrefs.SetInt("UnlockedLevels", 1);
            }


            if (sM.levelNum == 3 && PlayerPrefs.GetInt("UnlockedLevels", 0) < 2)
            {
                PlayerPrefs.SetInt("UnlockedLevels", 2);
            }

        }
    }


    void EndGameStatus()
    {
        if (PlayerPrefs.GetInt("LastLevel", 2) < 4)
        {
            if (PlayerPrefs.GetInt("win") == 1)
            {
                winText.text = "VICTORY";
                continueButton.SetActive(true);
                retryButton.SetActive(false);
            }
            else
            {
                winText.text = "GAME OVER";
                continueButton.SetActive(false);
                retryButton.SetActive(true);
            }



        }
        else
        {
            if (PlayerPrefs.GetInt("win") == 1)
            {
                winText.text = "VICTORY";
                continueButton.SetActive(false);
                retryButton.SetActive(false);
            }
            else
            {
                winText.text = "GAME OVER";
                continueButton.SetActive(false);
                retryButton.SetActive(true);
            }
        }

    }


    public void ProgressReset()
    {
        PlayerPrefs.SetInt("UnlockedLevels", 0);
    }
}
