using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBar : MonoBehaviour {

    public Sprite wave1;
    public Sprite wave2;
    public Sprite wave3;

    public Image waveImage;
    public Level level;

    void Start()
    {
        waveImage = transform.FindChild("WaveImage").GetComponent<Image>();
        level = FindObjectOfType<Level>();

    }


    // Update is called once per frame
    void Update () {

        switch (level.curWave) {
            case 1:
                waveImage.sprite = wave1;
                break;
            case 2:
                waveImage.sprite = wave2;
                break;
            case 3:
                waveImage.sprite = wave3;
                break;




        }

	}
}
