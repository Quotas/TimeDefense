using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[Header("Missle Style Attack Settings")]
	public int damage;
	public float speed;

	[HideInInspector]
	public Vector3 target;

	[Header("Area of Effect settings")]
	public bool aoeAttack;
	public float aoeAttackSpeed;
	public float aoeRange;

	void Start()
	{
		if (aoeAttack == true) {
			transform.localScale = Vector3.zero;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (aoeAttack == false) {
			
			transform.Translate (target * Time.deltaTime * speed);
		} 
		else {
			
			transform.localScale += new Vector3 (aoeAttackSpeed, aoeAttackSpeed, 0.0f) * Time.deltaTime;

			if (transform.localScale.x > aoeRange) {
				Destroy (gameObject);
			}

		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "Enemy") {
			
			if (aoeAttack == false) {
				Destroy (gameObject);
			}

			other.gameObject.GetComponent<Enemy> ().TakeDamage (damage);

		}

	}
		
}
