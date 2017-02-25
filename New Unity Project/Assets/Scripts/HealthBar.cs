using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public int curHealth = 5;
    public Sprite[] sprites;


    public void ChangeHealth(int dmg)
    {
        curHealth -= dmg;
        if (curHealth >= 1)
        {
            GetComponent<Image>().sprite = sprites[curHealth];

        }
        else
        {
            GetComponent<Image>().sprite = sprites[0];

        }

    }

}

