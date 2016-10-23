using UnityEngine;
using System.Collections;

public class RaycastMove : MonoBehaviour {


	public int moveEnabled = 0;
	public float fireRate = 3.5f;										// Number in seconds which controls how often the player can fire
	public Transform gunEnd;		//what do I need to change this to?					// Holds a reference to the gun end object, marking the muzzle location of the gun

	private Camera fpsCam;												// Holds a reference to the first person camera
	private WaitForSeconds shotDuration = new WaitForSeconds(3f); //maybe this should be the travel time or something. WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
	private AudioSource walkAudio;										// Reference to the audio source which will play our shooting sound effect
	private LineRenderer moveLine;										// Reference to the LineRenderer component which will display our laserline
	private float nextMove;	

	//Here are the move variables
	//var buildingObject = GameObject;
	//var myGround = Transform;
	public GameObject mainCamera;
	public ParticleSystem ShadowStepEffect;
	public int maxDistance = 10;

	// Use this for initialization
	void Start () {
	

		// Get and store a reference to our LineRenderer component
		moveLine = GetComponent<LineRenderer>();

		// Get and store a reference to our AudioSource component
		walkAudio = GetComponent<AudioSource>();

		// Get and store a reference to our Camera by searching this GameObject and its parents
		fpsCam = GetComponentInParent<Camera>();

		ShadowStepEffect = GetComponent<ParticleSystem> ();
	}
		


	void Update () {

		if (Input.GetButtonDown("Fire1") && Time.time > nextMove && moveEnabled==1) 
		{
			// Update the time when our player can move next
			nextMove = Time.time + fireRate;

			// Start our ShotEffect coroutine to turn our laser line on and off
			StartCoroutine (ShotEffect());
			checkForBuilding ();

			// Create a vector at the center of our camera's viewport
			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

			// Declare a raycast hit to store information about what our raycast has hit
			RaycastHit hit;

			// Set the start position for our visual effect for our laser to the position of gunEnd
			moveLine.SetPosition (0, gunEnd.position);

			// Check if our raycast has hit anything
			if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, maxDistance))
			{
				// Set the end position for our laser line 
				moveLine.SetPosition (1, hit.point);

				// Move player
				fpsCam.transform.position = hit.point;
				fpsCam.transform.Translate (0, 1, 0);
				ShadowStepEffect.transform.position = hit.point;

			}
			else
			{
				// If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
				moveLine.SetPosition (1, rayOrigin + (fpsCam.transform.forward * maxDistance));

			}
		}
	}
	private IEnumerator ShotEffect()
	{
		// Play the shooting sound effect
		walkAudio.Play ();

		// Turn on our line renderer
		moveLine.enabled = true;

		//Wait for .07 seconds
		yield return shotDuration;

		// Deactivate our line renderer after waiting
		moveLine.enabled = false;
	}

	private void checkForBuilding() {
		//var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
		RaycastHit hit;
		if (Physics.Raycast (rayOrigin,fpsCam.transform.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Floor")){
			//This is where i want to tell my buildingObject to move
			fpsCam.transform.position = hit.point;
			//This is where I start in on the code. I think i need to change the buildingobject to the player
			//The next thing I need to add in apparently is LERP,
		}
	}
}

