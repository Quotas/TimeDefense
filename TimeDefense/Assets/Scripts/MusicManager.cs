using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public AudioClip music;
    public AudioClip bell;
    public AudioClip snore;

    GameManager gameManager;

    // Use this for initialization
    void Start()
    {

        DontDestroyOnLoad(this);



        GetComponent<AudioSource>().clip = music;
        GetComponent<AudioSource>().Play(0);





    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "MainMenu" && GetComponent<AudioSource>().clip != music)
        {

            //GetComponent<AudioSource>().Stop();

            GetComponent<AudioSource>().clip = music;

            GetComponent<AudioSource>().Play(0);

        }


        if (gameManager == null && SceneManager.GetActiveScene().name == "Level")
        {


            gameManager = FindObjectOfType<GameManager>();

        }




        else if (gameManager != null && gameManager.state == GameManager.GameState.GAMEOVER)
        {

            if (GetComponent<AudioSource>().clip == music)
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = bell;
                GetComponent<AudioSource>().PlayOneShot(bell);
            }



        }
        else if (gameManager != null && gameManager.state == GameManager.GameState.WIN)
        {
            if (GetComponent<AudioSource>().clip == music)
            {

                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = snore;
                GetComponent<AudioSource>().PlayOneShot(snore);

            }



        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {


            Application.Quit();

        }



    }
}
