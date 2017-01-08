using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {
    
	public int startingHealth;
	public int currentHealth;

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
	}

	public void HurtPlayer(int damage)
	{
		currentHealth -= damage;

		playerController.KnockBack ();
	}
}