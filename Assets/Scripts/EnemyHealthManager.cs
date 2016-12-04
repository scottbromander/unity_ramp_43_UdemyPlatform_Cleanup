using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {
    
	public int startingHealth;
	private int currentHealth;

	public SpriteRenderer[] bodyParts;
	public float flashLenth;
	private float flashCounter;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0)
		{
			gameObject.SetActive(false);
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
	}

	public void Flash(){
		for (var i = 0; i < bodyParts.Length; i++) {
			bodyParts [i].material.SetFloat ("_FlashAmount", 1);
		}
	}
}