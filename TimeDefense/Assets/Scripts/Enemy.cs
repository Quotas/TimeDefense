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


    public int health = 1000;
    public int damage = 1;
    public float speed = 10f;

	public float deathTimer;
    public int coins;
    public bool dead;
	public float timer;

    #region EnemySprites
    public Sprite bigSprite;
    public Sprite fastSprite;
    public Sprite smallSprite;
    #endregion



    // Use this for initialization
    void Start()
    {
		
		timer = 0.0f;

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

		if (health <= 0) {

            if (dead == false)
                StartCoroutine(onDeath());


            dead = true;

           

		} else {
			transform.position = Vector3.MoveTowards (transform.position, nodes.First ().transform.position, speed * Time.deltaTime);
			
		}



    }

    public void TakeDamage(int damage)
    {

        health -= damage;

    }

    IEnumerator onDeath() {

        gameManager.AddCoins(coins); 

        gameObject.GetComponent<Animator>().SetBool("death", true);

        transform.position = new Vector3(transform.position.x, transform.position.y, 10);

        yield return new WaitForSeconds(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1f);
        Destroy(gameObject);
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


