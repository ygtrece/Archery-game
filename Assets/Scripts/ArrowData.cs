using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowData : MonoBehaviour {
	string collider_guy;		//record the name of collider
	private bool on_target;		//record whether the arrow is on the target
	private bool need_scored;	//record whether the arrow has been scored
	public float used_time;		//record the time when the arrow is used

	void Start() {
		init ();
	}

	//initial the arrow
	public void init() {
		collider_guy = null;
		on_target = false;
		need_scored = false;
	}

	public string getColliderGuy() { return collider_guy;}
	public void setColliderGuy(string guy) { collider_guy = guy;}
	public void resetColliderGuy() { collider_guy = null;}

	public bool getOnTarget() { return on_target;}
	public void setOnTarget() { on_target = true;}
	public void resetOnTarget() { on_target = false;}

	public bool getNeedScore() { return need_scored;}
	public void setNeedScore() { need_scored = true;}
	public void resetNeedScore() { need_scored = false;}

	public void destroyRigidBody() {
		Destroy (this.GetComponent<Rigidbody> ());
	}

	//the check of collision
	void OnTriggerEnter(Collider other) {
		if (this.getColliderGuy () == null) {
			if (on_target == false) {
				if (other.gameObject.tag.Contains ("circle")) {
					this.setColliderGuy (other.gameObject.tag);		//record which circle the arrow on
					Debug.Log (other.gameObject.tag);
					on_target = true;
					need_scored = true;
				}
			}
			if (on_target == true) {
				//destroy the component rigidbody of arrow so that it can stop on the target
				Destroy (this.GetComponent<Rigidbody> ());
			}
		}
	}

}
