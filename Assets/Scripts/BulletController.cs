﻿using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    
	public float bulletSpeed;

	private Rigidbody2D myRB;
	private PlayerController thePlayer;

	public int damageToGive;

	public GameObject impactEffect;

	public float sprayRange = 5.0f;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<PlayerController>();
		if(transform.position.x < thePlayer.transform.position.x)
		{
			bulletSpeed = -bulletSpeed;
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}

		sprayRange = Random.Range (-sprayRange, sprayRange);
	}
	
	// Update is called once per frame
	void Update () {

		myRB.velocity = new Vector2(bulletSpeed, sprayRange);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyHealthManager>().TakeDamage(damageToGive);
		}

		GameObject impact = (GameObject) Instantiate (impactEffect, transform.position, transform.rotation);
		impact.transform.localScale = transform.localScale;

		Destroy(gameObject);
	}
}