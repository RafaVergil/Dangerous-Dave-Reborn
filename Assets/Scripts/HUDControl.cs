using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDControl : MonoBehaviour {

	//Apenas classe pra controlar a interface.

	private static HUDControl _instance;
	public static HUDControl getInstance(){
		return _instance;
	}


	public Text txtHud;
	public GameObject trophyGottenBar;
	public GameObject dave1;
	public GameObject dave2;
	public GameObject dave3;
	public Text txtTransition;

	void Awake () {

		if (_instance != null && _instance != this) {
			Destroy (gameObject);
			return;
		} else {
			_instance = this;
		}

	}

	void Start(){
		DontDestroyOnLoad (gameObject);
	}

	public void UpdateHud(){
		if (DaveControl.getInstance ()) {
			txtHud.text = "SCORE: " + DaveControl.getInstance ().score + " LEVEL:" + DaveControl.getInstance ().currentLevel + " DAVES:";
			switch (DaveControl.getInstance ().lives) {
			default:
			case 3:
				dave1.SetActive (true);
				dave2.SetActive (true);
				dave3.SetActive (true);
				break;

			case 2:
				dave1.SetActive (true);
				dave2.SetActive (true);
				dave3.SetActive (false);
				break;

			case 1:
				dave1.SetActive (true);
				dave2.SetActive (false);
				dave3.SetActive (false);
				break;

			case 0:
				dave1.SetActive (false);
				dave2.SetActive (false);
				dave3.SetActive (false);
				break;
			}

			trophyGottenBar.SetActive (DaveControl.getInstance().hasTrophy ? true : false);
		}

		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName (Constants.SCENE_NAME_TRANSITION)) {
			txtTransition.gameObject.SetActive(false);	
		}

	}

	public void ConfigureHudForTransition(){
		trophyGottenBar.SetActive(false);
		txtTransition.text = "GOOD WORK! ONLY " + (Constants.MAX_LEVELS - DaveControl.getInstance().currentLevel + 1) + " MORE TO GO!";
		txtTransition.gameObject.SetActive(true);
	}
}
