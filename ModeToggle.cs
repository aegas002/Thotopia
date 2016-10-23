using UnityEngine;
using System.Collections;

public class ModeToggle : MonoBehaviour {

	//initialized these variables here because they weren't available when I placed them within start
	GameObject gun;

	RaycastShootComplete shootScript;
	RaycastMove moveScript;

	// Use this for initialization
	void Start () {

		gun = GameObject.Find("Gun");
		shootScript = gun.GetComponent<RaycastShootComplete>();
		moveScript = gun.GetComponent<RaycastMove>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableShoot() {

		shootScript.shootEnabled = 1;
		moveScript.moveEnabled = 0;


	}

	public void EnableMove() {

		shootScript.shootEnabled = 0;
		moveScript.moveEnabled = 1;
	}

}
