using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbulenceWind : MonoBehaviour {

	public float Turbulence = 0f;
	private float maxTurb;
	private float variation = 0.2f;



	void Awake () {
		maxTurb = this.GetComponent<WindZone> ().windTurbulence;
	}
	
	// Update is called once per frame
	void Update () {
		Turbulence += variation;

		if (Turbulence < 0f)
			variation = variation * -1f;

		if (Turbulence > maxTurb)
			variation = variation * -1f;
	}
}
