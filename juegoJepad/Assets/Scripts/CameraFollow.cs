using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;

	private Vector3 targetPosition;

	// ===========================
	void Start () {

	}
	// ===========================
	void Update () {
		if(this.target){
			this.targetPosition = this.target.position;
			this.targetPosition.z = -10;

			this.transform.position = Vector3.Lerp (this.transform.position,
			                                        this.targetPosition,
			                                        1);
		}

	}
}
