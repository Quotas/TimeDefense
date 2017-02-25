using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{



    public List<Enemy> activeEnemies;
    public List<Tower> activeTowers;

    public Dictionary<string, Enemy[]> waveID;

    public Enemy[] wave1;
    public Enemy[] wave2;
    public Enemy[] wave3;
    public Enemy[] wave4;

    public Sprite background;


    public bool waveFinished;
    public float waveDelay = 5f;
    public int curWave = 0;


    // Use this for initialization
    void Start()
    {

        waveID.Add("Wave 1", wave1);
        waveID.Add("Wave 2", wave2);
        waveID.Add("Wave 3", wave3);
        waveID.Add("Wave 4", wave4);


    }

    // Update is called once per frame
    void Update()
    {

        activeEnemies.AddRange(FindObjectsOfType<Enemy>());

        if (activeEnemies.Count == 0)
        {

            waveFinished = true;

        }

    }

    public void RemoveEnemy(Enemy enemy)
    {

        activeEnemies.Remove(enemy);


    }
}
