using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Level level;
    public enum EnemyType { BIG, SMALL, FAST };
    public EnemyType type;

    public List<Node> nodes;

    public int health;





    #region EnemySprites
    public Sprite bigSprite;
    public Sprite fastSprite;
    public Sprite smallSprite;
    #endregion



    // Use this for initialization
    void Start()
    {
        var tmpnodes = FindObjectsOfType<Node>();

        Array.Sort(tmpnodes);

        List<Node> nodes = new List<Node>(tmpnodes);

        switch (type)
        {

            case EnemyType.BIG:
                GetComponent<SpriteRenderer>().sprite = bigSprite;
                break;
            case EnemyType.FAST:
                GetComponent<SpriteRenderer>().sprite = fastSprite;
                break;
            case EnemyType.SMALL:
                GetComponent<SpriteRenderer>().sprite = smallSprite;
                break;


        }


    }

    // Update is called once per frame
    void Update()
    {

        if (health >= 0)
        {

            level.RemoveEnemy(this);

        }

        transform.Translate(Time.deltaTime * nodes.First().transform.position);

    }

    public void TakeDamage(int damage)
    {

        health -= damage;

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Node")
        {

            nodes.RemoveAt(0);

        }

        if (other.gameObject.tag == "EndNode")
        {

            Destroy(gameObject);


        }


    }

}


