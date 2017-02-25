using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Level level;
    public GameManager gameManager;
    public enum EnemyType { BIG, SMALL, FAST };
    public EnemyType type;

    public List<Node> nodes;


    public int health = 100;
    public int damage = 1;
    public float speed = 10f;


    #region EnemySprites
    public Sprite bigSprite;
    public Sprite fastSprite;
    public Sprite smallSprite;
    #endregion



    // Use this for initialization
    void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        nodes = FindObjectsOfType<Node>().OrderBy(node => node.order).ToList<Node>();

        //switch (type)
        //{

        //    case EnemyType.BIG:
        //        GetComponent<SpriteRenderer>().sprite = bigSprite;
        //        break;
        //    case EnemyType.FAST:
        //        GetComponent<SpriteRenderer>().sprite = fastSprite;
        //        break;
        //    case EnemyType.SMALL:
        //        GetComponent<SpriteRenderer>().sprite = smallSprite;
        //        break;


        //}


    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {

            level.RemoveEnemy(this);

        }

        transform.position = Vector3.MoveTowards(transform.position, nodes.First().transform.position, speed * Time.deltaTime);

    }

    public void TakeDamage(int damage)
    {

        health -= damage;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EndBounds")
        {

            gameManager.DoDamage(damage);

        }

        if (other.tag == "Node")
        {

            nodes.Remove(nodes.First());

        }

        if (other.tag == "EndNode")
        {

            Destroy(gameObject);


        }







    }

}


