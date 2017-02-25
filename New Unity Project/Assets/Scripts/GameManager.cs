using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class GameManager : MonoBehaviour

{
    [HideInInspector]
    public static GameManager instance = null;

    public Level[] levels;


    public int curLevel;



    // Use this for initialization
    void Awake()
    {

        if (instance == null)
        {

            instance = this;

        }

        if (instance != this)
        {
            Destroy(gameObject);

        }

        DontDestroyOnLoad(this);



    }




    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Main")
        {

            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);


        }


    }

}
