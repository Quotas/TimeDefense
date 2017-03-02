using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // Reference to objects
    public GameManager gameManager;
    public Level level;
    public Background background;
    public Clickable clicker;
    public Selection selector;
    public TierBadge badge;

    public AudioClip upgrade;

    // Types of Towers and 
    public enum TowerType { BASE, TEDDY, SLING, CANNON, SPEAKER }

    [Header("Tower Types and Projectiles")]
    public Sprite[] towerGraphics;
    [Tooltip("Make sure to ensure the order or towers and Projectiles are the same")]
    public Projectile[] towerWeapons;
    [Tooltip("Make sure to ensure the order or towers and Projectiles are the same")]
    public float[] towerRate;
    [Tooltip("Edit Make sure to ensure the order or towers and Projectiles are the same")]
    public bool[] isAOEWeapon;


    // Structure for applying damage and range settings 
    public struct Tier
    {
        public Tier(Vector3 stats)
        {
            damage = (int)stats.x;
            range = (int)stats.y;
            upgradeCost = (int)stats.z;
        }

        public int damage;
        public int range;
        public int upgradeCost;

    }

    [Header("Upgrade Modifiers (X is damage, Y is range, Z is cost)")]
    [Tooltip("Level 1")]
    public Vector3 tier1;
    [Tooltip("Level 2")]
    public Vector3 tier2;
    [Tooltip("Level 3")]
    public Vector3 tier3;

    // Private leave.
    public Tier m_Level1;
    public Tier m_Level2;
    public Tier m_Level3;


    // Tower properties
    public TowerType towerType;
    public Projectile towerWeapon;
    public Tier towerTier;
    public List<Enemy> m_enemyList;
    public int[] cost;
    public Tier tier;
    public float timer;

    public bool selected = false;

    // Tower Functionality
    private CircleCollider2D m_towerRange;


    // Use this for initialization
    void Start()
    {

        gameManager = FindObjectOfType<GameManager>();


        towerType = TowerType.BASE;
        m_Level1 = new Tier(tier1);
        m_Level2 = new Tier(tier2);
        m_Level3 = new Tier(tier3);

        towerTier = m_Level1;

        m_towerRange = GetComponent<CircleCollider2D>();
        background = FindObjectOfType<Background>();

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)transform.position.y;


        if (transform.Find("Clickable") != null)
        {

            clicker = transform.FindChild("Clickable").GetComponent<Clickable>();
            clicker.parent = this;


        }

        if (transform.Find("Selector") != null)
        {

            selector = transform.FindChild("Selector").GetComponent<Selection>();
            selector.parent = this;


        }

        if (transform.Find("TierBadge") != null)
        {

            badge = transform.FindChild("TierBadge").GetComponent<TierBadge>();
            badge.parent = this;


        }


        timer = 0.0f;


        ChangeType(towerType);
        //just for debug
    }


    // Update is called once per frame
    void Update()
    {


        switch (towerType)
        {

            case TowerType.BASE:

                break;
            case TowerType.SLING:

                if (timer > towerRate[(int)TowerType.SLING])
                {
                    towerFire();

                }

                break;

            case TowerType.SPEAKER:

                if (timer > towerRate[(int)TowerType.SPEAKER])
                {
                    towerFire();
                }

                break;

            case TowerType.CANNON:

                if (timer > towerRate[(int)TowerType.CANNON])
                {
                    towerFire();
                }

                break;

            case TowerType.TEDDY:

                if (timer > towerRate[(int)TowerType.TEDDY])
                {
                    towerFire();
                }

                break;
        }

        timer += Time.deltaTime;

    }

    void towerFire()
    {
        if (isAOEWeapon[(int)towerType] == false)
        {
            TowerFireProjectile();
        }
        else
        {
            TowerFireAreaEffect();
        }
    }


    public void Deselect()
    {

        gameObject.tag = "Untagged";
        selected = false;


    }

    public void OnClick()
    {
        if (background.curSelected != null)
            background.curSelected.Deselect();

        this.tag = "Selected";
        selected = true;

        background.curSelected = this;

        //Debug.Log("I have been hit! :", gameObject);


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
                return;
            }



            if (target.dead == true)
            {
                m_enemyList.Remove(target);
                return;
            }

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
            toInstanciate.GetComponent<CircleCollider2D>().radius = m_towerRange.radius / 2;
            // toInstanciate.target = m_enemyList.First ();
            Instantiate(toInstanciate);

            // reset the timer
            timer = 0.0f;

        }
    }


    public void ChangeType(TowerType newTowerType)
    {

        if (towerType != newTowerType)
        {


            if (newTowerType != TowerType.BASE)
            {

                if (!gameManager.CanBuy(cost[(int)newTowerType]))
                {

                    //Debug.Log("Cant afford to buy tower");
                    return;

                }

                gameManager.Buy(cost[(int)newTowerType]);
                towerType = newTowerType;

                switch (towerType)
                {
                    case TowerType.BASE:
                        gameObject.GetComponent<SpriteRenderer>().sprite = towerGraphics[0];
                        towerWeapon = towerWeapons[0];
                        break;
                    case TowerType.SLING:
                        gameObject.GetComponent<SpriteRenderer>().sprite = towerGraphics[1];
                        towerWeapon = towerWeapons[1];
                        break;
                    case TowerType.CANNON:
                        gameObject.GetComponent<SpriteRenderer>().sprite = towerGraphics[2];
                        towerWeapon = towerWeapons[2];
                        break;
                    case TowerType.SPEAKER:
                        gameObject.GetComponent<SpriteRenderer>().sprite = towerGraphics[3];
                        towerWeapon = towerWeapons[3];
                        break;
                    case TowerType.TEDDY:
                        gameObject.GetComponent<SpriteRenderer>().sprite = towerGraphics[4];
                        towerWeapon = towerWeapons[4];
                        break;

                }

                // setup the range

            }

            m_towerRange.radius += towerTier.range;
        }

    }



    public void Reset()
    {

        // For if the player sells the tower

        towerType = TowerType.BASE;
        towerTier = m_Level1;

    }


    public void UpgradeTower()
    {


        if (towerTier.Equals(m_Level3) == false)
        {

            if (towerTier.Equals(m_Level1) && gameManager.CanBuy(m_Level2.upgradeCost))
            {
                //GetComponent<AudioSource>().PlayOneShot(upgrade);
                towerTier = m_Level2;
                gameManager.Buy(m_Level2.upgradeCost);
            }
            else if (towerTier.Equals(m_Level2) && gameManager.CanBuy(m_Level3.upgradeCost))
            {
                //GetComponent<AudioSource>().PlayOneShot(upgrade);
                towerTier = m_Level3;
                gameManager.Buy(m_Level3.upgradeCost);
            }

        }


        //Debug.Log("Tower Upgraded");


    }


}
