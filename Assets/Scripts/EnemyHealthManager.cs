using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {
    
	public int startingHealth;
	private int currentHealth;

	public SpriteRenderer[] bodyParts;
	public float flashLenth;
	private float flashCounter;

	private EnemyController enemyController;
	private Animator anim;
	private Rigidbody2D myRB;

	public float deathSpin = 10.0f;

	public Sprite[] brokenParts;
	private bool dead = false;

	public GameObject explosion;

	public Rigidbody2D[] RBParts;
	public float explosionForce;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		enemyController = GetComponent<EnemyController> ();
		anim = GetComponent<Animator> ();
		myRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0 && !dead)
		{
			//gameObject.SetActive(false);
			enemyController.enabled = false;
			anim.enabled = false;
			myRB.constraints = RigidbodyConstraints2D.None;
			myRB.AddTorque (deathSpin * -transform.localScale.x);

			gameObject.layer = LayerMask.NameToLayer ("Dead");

			for (int i = 0; i < bodyParts.Length; i++) {
				bodyParts [i].sprite = brokenParts [i];
			}

			Instantiate (explosion, transform.position, transform.rotation);

			for (int i = 0; i < RBParts.Length; i++) {
				RBParts [i].isKinematic = false;
				RBParts [i].AddTorque (deathSpin);
				RBParts [i].velocity = new Vector2 (Random.Range (-explosionForce, explosionForce), 
					Random.Range (-explosionForce, explosionForce));
			}

			dead = true;
		}

		if (flashCounter > 0) {
			flashCounter -= Time.deltaTime;

			if (flashCounter <= 0) {
				UnFlash ();
			}
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		enemyController.KnockBack ();
		Flash ();
	}

	public void Flash(){
		for (var i = 0; i < bodyParts.Length; i++) {
			bodyParts [i].material.SetFloat ("_FlashAmount", 1);
		}
		flashCounter = flashLenth;
	}

	public void UnFlash(){
		for (var i = 0; i < bodyParts.Length; i++) {
			bodyParts [i].material.SetFloat ("_FlashAmount", 0);
		}
	}
}