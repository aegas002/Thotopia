using UnityEngine;
using System.Collections;

public class ShootableBox : MonoBehaviour {

	//The box's current health point total
	public int currentHealth = 3;

	AudioSource enemyAudio;
	public AudioClip deathClip;

	void Awake ()
	{
		// Setting up the references.
		enemyAudio = GetComponent <AudioSource> ();


		// Setting the current health when the enemy first spawns.
		//currentHealth = startingHealth;
	}

	public void Damage(int damageAmount)
	{
		//subtract damage amount when Damage function is called
		currentHealth -= damageAmount;
		enemyAudio.Play ();
		//Check if health has fallen below zero
		if (currentHealth <= 0) 
		{
			GetComponent <NavMeshAgent> ().enabled = false;

			// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
			GetComponent <Rigidbody> ().isKinematic = true;

			transform.Rotate (90,90,90);
			enemyAudio.clip = deathClip;
			enemyAudio.volume = .2f;
			enemyAudio.Play ();
			transform.Rotate (180,180,180);
			Destroy (gameObject, 2f);
			//if health has fallen below zero, deactivate it 
			//gameObject.SetActive (false);
		}
	}
}
