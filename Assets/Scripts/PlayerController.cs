﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
	public float moveSpeed;
	public float jumpSpeed;

	private Rigidbody2D myRB;

	private Animator anim;

	public GameObject bullet;
	public Transform bulletPoint;

	public Transform groundPoint;
	public LayerMask whatIsGround;
	public float groundRadius;

	public bool grounded;

	public AudioSource soundJump;
	public AudioSource soundShoot;

	public float waitBetweenShots;
	private float betweenShotCounter;

	public GameObject muzzleFlash;

	private CameraController cameraController;
	public float shakeAmount;

	public float knockbackForce;
	public float knockbackDuration;
	private float knockbackCounter;

	[HideInInspector]
	public bool knockBack;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		cameraController = FindObjectOfType<CameraController> ();
	}

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update () {

		muzzleFlash.SetActive (false);

		if (knockBack) {
			knockbackCounter -= Time.deltaTime;
			myRB.velocity = new Vector2 (-knockbackForce * transform.localScale.x, myRB.velocity.y);

			if (knockbackCounter <= 0) {
				knockBack = false;
			}
		} else {
			myRB.velocity = new Vector2 (Input.GetAxis ("Horizontal") * moveSpeed, myRB.velocity.y);

			if (Input.GetButtonDown ("Jump") && grounded) {
				myRB.velocity = new Vector2 (myRB.velocity.x, jumpSpeed);
				soundJump.Play ();
			}

			if (Input.GetAxisRaw ("Horizontal") > 0 && transform.localScale.x < 0)
				transform.localScale = new Vector3 (1f, 1f, 1f);
			if (Input.GetAxisRaw ("Horizontal") < 0 && transform.localScale.x > 0)
				transform.localScale = new Vector3 (-1f, 1f, 1f);
		
			if (Input.GetButtonDown ("Fire1")) {
				fireBullet ();
			}

			if (Input.GetButton ("Fire1")) {
				betweenShotCounter -= Time.deltaTime;
				if (betweenShotCounter <= 0) {
					fireBullet ();
				}
			}
		}

		anim.SetFloat("Speed", Mathf.Abs(myRB.velocity.x));
		anim.SetBool("Grounded", grounded);
	}

	void fireBullet(){
		Instantiate(bullet, bulletPoint.position, transform.rotation);
		soundShoot.Play ();

		betweenShotCounter = waitBetweenShots;

		muzzleFlash.SetActive (true);

		cameraController.ScreenShake (shakeAmount);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Debug.Log("On Enemy");
			myRB.velocity = new Vector2(myRB.velocity.x, jumpSpeed * 0.75f);
		}
	}

	public void KnockBack(){
		knockbackCounter = knockbackDuration;
		myRB.velocity = new Vector2 (-knockbackForce * transform.localScale.x, knockbackForce);
		knockBack = true;
	}
}