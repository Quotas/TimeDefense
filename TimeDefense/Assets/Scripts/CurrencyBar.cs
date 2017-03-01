using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyBar : MonoBehaviour
{

    // Use this for initialization

    public Text currencyText;

    public int curCurrency;

    void Start()
    {
        currencyText = transform.Find("Coins").GetComponent<Text>();
        curCurrency = 0;


    }

    // Update is called once per frame
    void Update()
    {

        currencyText.text = curCurrency.ToString();



    }
}
