using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	// Reference to objects
	public Level level;
	public Tier tier; 

	// Types of Towers and 
	public enum Type {Base, Type2, Type3}; 
	public Sprite[] towerGraphics;
	public Projectile[] towerWeapons;

	// possibly temp
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


	public Vector2 tier1;
	public Vector2 tier2;
	public Vector2 tier3;
	private Tier m_Level1;
	private Tier m_Level2;
	private Tier m_Level3;


	// Tower properties
	public Type towerType;
	public Projectile towerWeapon;
	public Tier towerTier;
	private List<GameObject> m_enemyList;


	// Tower Functionality
	private CircleCollider2D m_towerRange;


	// Use this for initialization
	void Start () 
	{
		towerType = Type.Base;

		m_Level1 = new Tier(tier1);
		m_Level2 = new Tier(tier2);
		m_Level3 = new Tier(tier3);

		towerTier = m_Level1;
	}


	// Update is called once per frame
	void Update () 
	{

		// Get the tpye change from the user


		switch (towerType) 
		{
		case Type.Base:



			break;
		case Type.Type2:



			break;

		case Type.Type3:



			break;
		}

		
	}


	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.tag == "Enemy") 
		{

			m_enemyList.Add (other.gameObject);

		}

	}


	void OnTriggerLeave2D(Collider2D other)
	{

		if (other.gameObject.tag == "Enemy") 
		{

			m_enemyList.Remove (other.gameObject);

		}

	}


	void TowerFireProjectile()
	{

		Transform target = null;
	
		if (m_enemyList.Count > 0) {
		
			// fire at the enemy
			if (m_enemyList[0] != null)
			{
				target = m_enemyList [0].transform;
			}
		
		}

		if (target != null) 
		{
			Quaternion lookat = Quaternion.LookRotation (target.position - transform.position);

			Projectile toInstanciate; 
			toInstanciate = Instantiate (towerWeapon, transform.position, lookat);
		}

	}


	void TowerFireAreaEffect()
	{

		for (int i = 0; i < m_enemyList.Count; i++) {

			m_enemyList [i].TakeDamage (towerTier.damage);
		
		}
		
	}


	public void ChangeType(Type newTowerType)
	{

		towerType = newTowerType;

		switch (newTowerType) {
		case Type.Base:
			gameObject.GetComponent<SpriteRenderer> ().sprite = towerGraphics [0];
			towerWeapon = towerWeapons [0];
			break;
		case Type.Type2:
			gameObject.GetComponent<SpriteRenderer> ().sprite = towerGraphics [1];
			towerWeapon = towerWeapons [1];
			break;
		case Type.Type3:
			gameObject.GetComponent<SpriteRenderer> ().sprite = towerGraphics [2];
			towerWeapon = towerWeapons [2];
			break;

		}
	}


	public void reset()
	{

		towerType = Type.Base;
		towerTier = m_Level1;

	}

}
