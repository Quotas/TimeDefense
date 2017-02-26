using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // Reference to objects
    public Level level;
    public Background background;
    public Clickable clicker;

    // Types of Towers and 
    public enum Type { Base, Type2, Type3 }

    [Header("Tower Types and Projectiles")]
    public Sprite[] towerGraphics;
    [Tooltip("Make sure to ensure the order or towers and Projectiles are the same")]
    public Projectile[] towerWeapons;
    [Tooltip("Make sure to ensure the order or towers and Projectiles are the same")]
    public float[] towerRate;


    // Structure for applying damage and range settings 
    public struct Tier
    {
        public Tier(Vector2 stats)
        {
            damage = (int)stats.x;
            range = (int)stats.y;
        }

        public int damage;
        public int range;

    }

    [Header("Upgrade Modifiers (X is damage, Y is range")]
    [Tooltip("Level 1")]
    public Vector2 tier1;
    [Tooltip("Level 2")]
    public Vector2 tier2;
    [Tooltip("Level 3")]
    public Vector2 tier3;

    // Private leave.
    private Tier m_Level1;
    private Tier m_Level2;
    private Tier m_Level3;


    // Tower properties
    public Type towerType;
    public Projectile towerWeapon;
    public Tier towerTier;
    public List<Enemy> m_enemyList;
    public Tier tier;
    public float timer;
    public bool isAOEWeapon;

    public bool selected = false;

    // Tower Functionality
    private CircleCollider2D m_towerRange;


    // Use this for initialization
    void Start()
    {
        towerType = Type.Base;
        m_Level1 = new Tier(tier1);
        m_Level2 = new Tier(tier2);
        m_Level3 = new Tier(tier3);

        towerTier = m_Level1;

        m_towerRange = GetComponent<CircleCollider2D>();
        background = FindObjectOfType<Background>();

        ChangeType(Type.Type2);

        if (transform.Find("Clickable") != null)
        {

            clicker = transform.Find("Clickable").GetComponent<Clickable>();
            clicker.parent = this;


        }
        timer = 0.0f;

        //just for debug
    }


    // Update is called once per frame
    void Update()
    {

        // Get the tpye change from the user
        // menu interfation 
        // firing at the enemies


        switch (towerType)
        {

            case Type.Base:

                if (timer > towerRate[(int)Type.Base])
                {
                    if (isAOEWeapon == false)
                    {
                        TowerFireProjectile();
                    }
                    else
                    {
                        TowerFireAreaEffect();
                    }
                }

                break;

            case Type.Type2:

                if (timer > towerRate[(int)Type.Type2])
                {
                    if (isAOEWeapon == false)
                    {
                        TowerFireProjectile();
                    }
                    else
                    {
                        TowerFireAreaEffect();
                    }

                }

                break;

            case Type.Type3:

                if (timer > towerRate[(int)Type.Type3])
                {
                    if (isAOEWeapon == false)
                    {
                        TowerFireProjectile();
                    }
                    else
                    {
                        TowerFireAreaEffect();
                    }
                }

                break;
        }

        timer += Time.deltaTime;

    }


    public void Deselect()
    {

        gameObject.tag = "Untagged";
        selected = false;


    }

    public void OnClick()
    {
        this.tag = "Selected";
        selected = true;

        background.curSelected = this;

        Debug.Log("I have been hit! :", gameObject);


    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {

            m_enemyList.Add(other.gameObject.GetComponent<Enemy>());

        }

    }


    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {

            m_enemyList.Remove(m_enemyList.First());

        }

    }


    void TowerFireProjectile()
    {

        Enemy target = null;

        if (m_enemyList.Count > 0)
        {

            // fire at the enemy
            if (m_enemyList.First() != null)
            {
                target = m_enemyList.First();
            }
            else
            {

                m_enemyList.Remove(m_enemyList.First());
            }

            if (target == null)
            {
                return;

            }
            Quaternion lookat = Quaternion.LookRotation(target.transform.position - transform.position);

            // Set the parameters to fire at the unit.
            Projectile toInstanciate = towerWeapon;
            toInstanciate.transform.position = transform.position;
            toInstanciate.damage = towerTier.damage;
            toInstanciate.target = m_enemyList.First();
            Instantiate(toInstanciate);

            // reset the timer
            timer = 0.0f;

        }


    }


    void TowerFireAreaEffect()
    {
        if (m_enemyList.Count > 0)
        {

            Projectile toInstanciate = towerWeapon;
            toInstanciate.transform.position = transform.position;
            toInstanciate.damage = towerTier.damage;
            // toInstanciate.target = m_enemyList.First ();
            Instantiate(toInstanciate);

            // reset the timer
            timer = 0.0f;

        }
    }


    public void ChangeType(Type newTowerType)
    {

        towerType = newTowerType;

        switch (towerType)
        {
            case Type.Base:
                gameObject.GetComponent<SpriteRenderer>().sprite = towerGraphics[0];
                towerWeapon = towerWeapons[0];
                break;
            case Type.Type2:
                gameObject.GetComponent<SpriteRenderer>().sprite = towerGraphics[1];
                towerWeapon = towerWeapons[1];
                break;
            case Type.Type3:
                gameObject.GetComponent<SpriteRenderer>().sprite = towerGraphics[2];
                towerWeapon = towerWeapons[2];
                break;

        }

        // setup the range

        m_towerRange.radius = towerTier.range;
    }


    public void reset()
    {

        // For if the player sells the tower

        towerType = Type.Base;
        towerTier = m_Level1;

    }

}
