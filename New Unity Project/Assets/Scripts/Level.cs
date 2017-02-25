using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Level : MonoBehaviour
{



    public List<Enemy> activeEnemies;
    public List<Tower> activeTowers;

    public Dictionary<int, Enemy[]> waveID = new Dictionary<int, Enemy[]>();

    public Enemy[] wave1;
    public Enemy[] wave2;
    public Enemy[] wave3;
    public Enemy[] wave4;

    public Sprite background;



    public bool waveFinished;
    public bool inDelay;
    public float waveDelay = 5f;
    public int curWave = 1;


    // Use this for initialization
    void Start()
    {

        waveID.Add(1, wave1);
        waveID.Add(2, wave2);
        waveID.Add(3, wave3);
        waveID.Add(4, wave4);

        SpawnWave();


    }

    // Update is called once per frame
    void Update()
    {

        if (waveFinished == false)
        {

            activeEnemies.AddRange(FindObjectsOfType<Enemy>());

        }

        if (activeEnemies.Count == 0 && inDelay == false)
        {

            waveFinished = true;
            if (curWave != 4)
            {
                curWave += 1;
                Invoke("SpawnWave", waveDelay);
                inDelay = true;
            }
            else
            {
                Debug.Log("You won?");

            }



        }

    }

    public void RemoveEnemy(Enemy enemy)
    {

        activeEnemies.Remove(enemy);


    }

    void SpawnWave()
    {
        var xOffset = 0;
        inDelay = false;

        foreach (Enemy enemy in waveID[curWave])
        {
            xOffset += 2;
            enemy.level = this;
            enemy.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
            enemy.transform.position += new Vector3(xOffset, 0, 0);
            Instantiate(enemy);

        }

        waveFinished = false;



    }


}
