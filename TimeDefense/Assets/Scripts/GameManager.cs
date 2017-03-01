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

    public enum GameState { GAMEOVER, PLAYING, WIN, PAUSED }
    public GameState state;

    public Scene levelScene;
    public Scene GUIScene;

    public int curLevel;
    public HealthBar healthBar;
    public CurrencyBar currencyBar;
    public GameOver gameOverText;

    public Button teddyButton;
    public Button slingButton;
    public Button cannonButton;
    public Button speakerButton;
    public Button upgradeButton;

	public Background background;
    public Tower curTower;

    // Use this for initialization
    void Start()
    {

        levelScene = SceneManager.GetSceneByBuildIndex(0);

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
        currencyBar = FindObjectOfType<CurrencyBar>();

        teddyButton = FindObjectOfType<Canvas>().transform.FindChild("ButtonManager").FindChild("Teddy").GetComponent<Button>();
        slingButton = FindObjectOfType<Canvas>().transform.FindChild("ButtonManager").FindChild("Slinger").GetComponent<Button>();
        cannonButton = FindObjectOfType<Canvas>().transform.FindChild("ButtonManager").FindChild("Pillow").GetComponent<Button>();
        speakerButton = FindObjectOfType<Canvas>().transform.FindChild("ButtonManager").FindChild("Speaker").GetComponent<Button>();

        upgradeButton = FindObjectOfType<Canvas>().transform.FindChild("ButtonManager").FindChild("Upgrade").GetComponent<Button>();

        background = FindObjectOfType<Background>();


        state = GameState.PLAYING;




    }




    // Update is called once per frame
    void Update()
    {

       


        if (curTower != background.curSelected)
        {


            teddyButton.onClick.RemoveAllListeners();
            slingButton.onClick.RemoveAllListeners();
            cannonButton.onClick.RemoveAllListeners();


            foreach (Transform child in FindObjectOfType<Canvas>().transform.FindChild("ButtonManager"))
            {

                child.GetComponent<Button>().interactable = true;

            }

            curTower = background.curSelected;

            teddyButton.onClick.AddListener(() => curTower.ChangeType(Tower.TowerType.TEDDY));
            slingButton.onClick.AddListener(() => curTower.ChangeType(Tower.TowerType.SLING));
            cannonButton.onClick.AddListener(() => curTower.ChangeType(Tower.TowerType.CANNON));
            speakerButton.onClick.AddListener(() => curTower.ChangeType(Tower.TowerType.SPEAKER));

            upgradeButton.onClick.AddListener(() => curTower.UpgradeTower());

        }


        if (curTower == null)
        {

            foreach (Transform child in FindObjectOfType<Canvas>().transform.FindChild("ButtonManager"))
            {

                child.GetComponent<Button>().interactable = false;


            }


        }


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //var select = GameObject.FindWithTag("select").transform;
            if (Physics.Raycast(ray, out hit))
            {

                Debug.Log(hit.transform.tag);

            }
        }




        switch (state)
        {

            case GameState.PLAYING:
                break;
            case GameState.WIN:
                gameOverText.GetComponent<Image>().sprite = gameOverText.win; 
                gameOverText.GetComponent<Image>().enabled = true;
                Time.timeScale = 0;
                break;
            case GameState.GAMEOVER:
                gameOverText.GetComponent<Image>().sprite = gameOverText.lose;
                gameOverText.GetComponent<Image>().enabled = true;
                Time.timeScale = 0;
                break;

        }


    }


    public bool CanBuy(int amount)
    {

        if (currencyBar.curCurrency - amount >= 0)
        {

            return true;
        }
        else {
            return false;
        }


    }

    public void Buy(int amount) {

        currencyBar.curCurrency -= amount; 

    }

    public void AddCoins(int amount)
    {


        currencyBar.curCurrency += amount;


    }

    public void Pause()
    {

        Time.timeScale = 0;

    }

    public void UnPause()
    {

        Time.timeScale = 1;

    }


     public void SpeedUp()
    {


		if (Time.timeScale == 2)
		{
			Time.timeScale = 1;
		} else
		{
			Time.timeScale = 2;
		}

	}

	public void PlayPause()
	{

		if (state == GameState.PLAYING)
		{
			Pause ();
			state = GameState.PAUSED;
		} else
		{
			UnPause ();
			state = GameState.PLAYING;
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
