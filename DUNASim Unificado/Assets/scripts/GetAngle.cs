using UnityEngine;
using System.Collections;

// helper script for testing angle calculation
// USAGE:
// - Attach this script to objectA and assign objectB as target
// - Then select objectA and move it around ObjectB and you can see angle values in inspector

[ExecuteInEditMode]
public class GetAngle : MonoBehaviour 
{
	public Transform target;
	public float angle;

	void Update () 
	{
		if (!target) return;

		var myPos = transform.position;
		myPos.y = 0;

		var targetPos = target.position;
		targetPos.y = 0;

		Vector3 toOther = (myPos - targetPos).normalized;

		angle = Mathf.Atan2(toOther.z, toOther.x) * Mathf.Rad2Deg + 180;

		//Debug.DrawLine (myPos, targetPos, Color.yellow);
	}

}