using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{



    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is null!");
            }
            return _instance;
        }
    }

 
    UiManager _UM;
    sceneManager _SM;


    private void Awake()
    {
        _instance = this;

        _SM = FindObjectOfType<sceneManager>();
        _UM = FindObjectOfType<UiManager>();

    }







    public sceneManager SM { get { return _SM; } set { _SM = value; } }
    public UiManager UM { get { return _UM; } set { _UM = value; } }


}
