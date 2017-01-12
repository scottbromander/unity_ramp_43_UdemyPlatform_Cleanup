using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {
    
	public int startingHealth;
	public int currentHealth;

	public SpriteRenderer[] bodyParts;
	public float flashLenth;
	private float flashCounter;

	private PlayerController playerController;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		playerController = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0)
		{
			gameObject.SetActive(false);
		}

		if (flashCounter > 0) {
			flashCounter -= Time.deltaTime;

			if (flashCounter <= 0) {
				UnFlash ();
			}
		}
	}

	public void HurtPlayer(int damage)
	{
		if (!playerController.knockBack) {
			currentHealth -= damage;
			playerController.KnockBack ();
			Flash ();
		}
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