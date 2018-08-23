using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControl : MonoBehaviour {

	public GameObject dave;
	public float speed = 2.5f;
	public float rightLimit = 9f;

	void Start(){
		if (DaveControl.getInstance ()) {
			DaveControl.getInstance ().gameObject.SetActive (false);
			DaveControl.getInstance ().hasTrophy = false;
		}
		if (HUDControl.getInstance()) {
			HUDControl.getInstance().ConfigureHudForTransition ();
		}
		dave.transform.position = new Vector2 (0, .46f);
	}

	void Update () {
		dave.transform.position += (Vector3.right * speed * Time.deltaTime);

		if (dave.transform.position.x > rightLimit) {
			SceneManager.LoadScene (Constants.SCENE_NAME_STAGES + DaveControl.getInstance ().currentLevel);
		}
	}
}
