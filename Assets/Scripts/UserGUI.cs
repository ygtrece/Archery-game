using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
	SceneController MySceneController;

	void Start () {
		MySceneController = (SceneController)FindObjectOfType (typeof(SceneController));
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("mouse down");

			//get the direction of sending an arrow
			Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			MySceneController.sendArrow (mouseRay.direction);
		}
	}

	void OnGUI() {

		//the style of text
		GUIStyle fontStyle = new GUIStyle ();
		fontStyle.normal.textColor = new Color (230, 231, 158);
		fontStyle.fontSize = 20;

		GUI.Label (new Rect (100, 120, 400, 200), "Your Score: " + MySceneController.getScore (), fontStyle);
		if (MySceneController.getWind () < 0) {
			GUI.Label (new Rect (270, 120, 400, 200), "Wind: " + (-MySceneController.getWind ()) + " <<", fontStyle);
		}
		else if (MySceneController.getWind () > 0) {
			GUI.Label (new Rect (270, 120, 400, 200), "Wind: " + ">> " + MySceneController.getWind (), fontStyle);
		}
		else {
			GUI.Label (new Rect (270, 120, 400, 200), "Wind: " + MySceneController.getWind (), fontStyle);
		}
	}
}
