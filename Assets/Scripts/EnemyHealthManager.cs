﻿using UnityEngine;
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

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		enemyController = GetComponent<EnemyController> ();
		anim = GetComponent<Animator> ();
		myRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0)
		{
			//gameObject.SetActive(false);
			enemyController.enabled = false;
			anim.enabled = false;
			myRB.constraints = RigidbodyConstraints2D.None;
			myRB.AddTorque (deathSpin);

			gameObject.layer = LayerMask.NameToLayer ("Dead");
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