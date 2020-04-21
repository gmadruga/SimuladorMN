using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vento : MonoBehaviour {

	private Rigidbody barcRB;

	public float windforce;

	// Use this for initialization
	void Awake () {
		barcRB = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider other) {
		if (other.CompareTag ("Wind")) {

			Vector3 resultante;
			resultante.x = 0f;
			resultante.y = 0f;
			resultante.z = windforce;

			barcRB.AddForce (resultante);
		}
	}
}
