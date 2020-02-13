using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
	Factory MyFactory;
	WindController windController;

	void Start() {
		if (MyFactory == null) {
			MyFactory = new Factory ();
		}
		MyFactory.init ();
		if (windController == null) {
			windController = new WindController ();
		}
		windController.windChange ();
	}

	//every time check are there any arrows should be recycled and check the score
	void Update() {
		MyFactory.recycleCheck ();
		MyFactory.scoreCheck ();
	}

	public void sendArrow(Vector3 direction) {
		Vector3 wind_direction = windController.getDirection ();
		int wind = windController.getWind ();
		Debug.Log (wind);
		MyFactory.sendArrow (direction, wind_direction, wind);
		windController.windChange ();		//after sending an arrow,change the wind
	}

	public int getScore() { return MyFactory.getScore();}
	public int getWind() { return windController.getWind ();}
}

public class WindController {
	private int wind;
	private int wind_range = 40;  //the largest wind
	private Vector3 direction = new Vector3(1, 0, 0);  //ensure the wind is only on the X asix

	public int getWind() { return wind;}
	public Vector3 getDirection() { return direction;}

	public void windChange() {
		wind = Random.Range (-wind_range, wind_range);
	}

}