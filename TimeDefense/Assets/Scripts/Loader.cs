using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{

    public GameManager gameManager;
    // Use this for initialization
    void Start()
    {

        if (gameManager == null)
        {

            Instantiate(gameManager);
        }

    }


}
