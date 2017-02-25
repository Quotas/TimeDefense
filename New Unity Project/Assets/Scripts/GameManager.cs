using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class GameManager : MonoBehaviour

{

    public static GameManager instance = null;

    public Level[] levels;

    public enum GameState { GAMEOVER, PLAYING, WIN }
    public GameState state;

    public int curLevel;
    public HealthBar healthBar;
    public GameOver gameOverText;



    // Use this for initialization
    void Start()
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


        healthBar = FindObjectOfType<HealthBar>();
        gameOverText = FindObjectOfType<GameOver>();

        state = GameState.PLAYING;



    }




    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Main")
        {

            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);


        }

        switch (state)
        {

            case GameState.PLAYING:
                break;
            case GameState.WIN:
                gameOverText.GetComponent<Text>().text = "WIN";
                gameOverText.GetComponent<Text>().enabled = true;
                break;
            case GameState.GAMEOVER:
                gameOverText.GetComponent<Text>().enabled = true;
                Time.timeScale = 0;
                break;

        }


    }


    public void DoDamage(int damage)
    {

        healthBar.ChangeHealth(damage);

        if (healthBar.curHealth <= 0)
        {

            state = GameState.GAMEOVER;

        }

    }

}
