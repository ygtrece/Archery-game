using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : System.Object {

	List<GameObject> Free_arrow = new List<GameObject>();
	List<GameObject> Used_arrow = new List<GameObject>();

	int score;

	Vector3 target_position = new Vector3(0, 3, 0);
	Vector3 plane_position = new Vector3(0, -3, 0);
	Vector3 init_position = new Vector3(0, 2, -15);
	Vector3 light_position = new Vector3(0, 3, -2);
	Vector3 frame_position = new Vector3(0, -1, 0.6f);

	public void init()
	{
		GameObject.Instantiate (Resources.Load ("Prefabs/target"), target_position, Quaternion.identity);
		GameObject.Instantiate (Resources.Load ("Prefabs/Plane"), plane_position, Quaternion.identity);
		GameObject frame = GameObject.Instantiate (Resources.Load ("Prefabs/Frame")) as GameObject;
		frame.transform.position = frame_position;
		GameObject light = GameObject.Instantiate (Resources.Load ("Prefabs/Directional Light")) as GameObject;
		light.transform.position = light_position;

		score = 0;
	}

	public void sendArrow(Vector3 direction, Vector3 wind_direction, int wind) {
		GameObject currentArrow;

		//if there are arrows in list, use one , or new an arrow
		if (Free_arrow.Count == 0) {
			currentArrow = GameObject.Instantiate (Resources.Load ("Prefabs/arrow")) as GameObject;
		}
		else {
			currentArrow = Free_arrow[0];
			Free_arrow.RemoveAt (0);
			currentArrow.AddComponent<Rigidbody> ();
		}
		currentArrow.SetActive (true);
		currentArrow.transform.position = init_position;
		currentArrow.transform.up = direction;														//ensure rotation of the arrow is the same as the direction of mouse
		currentArrow.GetComponent<Rigidbody> ().AddForce (direction * 30, ForceMode.Impulse);		//give a force to send an arrow
		currentArrow.GetComponent<Rigidbody> ().AddForce (wind_direction * wind, ForceMode.Force);	//the wind is regarded as a constant force
		Used_arrow.Add (currentArrow);
		currentArrow.GetComponent<ArrowData>().used_time = Time.time;
	}

	public void recycleCheck() {
		if (Used_arrow.Count != 0) {
			float present = Time.time;
			if (present - Used_arrow[0].GetComponent<ArrowData> ().used_time >= 5) {
				recycleOneArrow ();
			}
		}
	}

	void recycleOneArrow() {
		GameObject currentArrow = Used_arrow[0];
		Used_arrow.RemoveAt (0);
		currentArrow.GetComponent<ArrowData> ().init ();
		currentArrow.SetActive (false);

		//the arrow send but never collide into the target should destroy the component rigidbody either
		if (currentArrow.GetComponent<ArrowData> ().getOnTarget () == false) {
			currentArrow.GetComponent<ArrowData> ().destroyRigidBody ();
		}

		Free_arrow.Add (currentArrow);
	}

	public void scoreCheck() {
		for (int i = 0; i < Used_arrow.Count; i++) {
			if (Used_arrow [i].GetComponent<ArrowData> ().getNeedScore () == true) {
				//add the score by the type of colliderGuy recored in ArrowData
				if (Used_arrow [i].GetComponent<ArrowData> ().getColliderGuy ()== "circle1") {
					score += 10;
				}
				else if (Used_arrow [i].GetComponent<ArrowData> ().getColliderGuy () == "circle2") {
					score += 8;
				}
				else if (Used_arrow [i].GetComponent<ArrowData> ().getColliderGuy () == "circle3") {
					score += 6;
				}
				else if (Used_arrow [i].GetComponent<ArrowData> ().getColliderGuy () == "circle4") {
					score += 4;
				}
				else if (Used_arrow [i].GetComponent<ArrowData> ().getColliderGuy () == "circle5") {
					score += 2;
				}
				else if (Used_arrow [i].GetComponent<ArrowData> ().getColliderGuy () == "circle6") {
					score += 1;
				}
				Used_arrow [i].GetComponent<ArrowData> ().resetNeedScore ();
			}
		}
	}

	public int getScore() { return score;}
}
