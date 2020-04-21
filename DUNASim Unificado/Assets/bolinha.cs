using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolinha : MonoBehaviour
{

	public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey ("w")) {
			
			gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.forward * 10);
		}
		if (Input.GetKey ("a")) {

			gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.left * 10);
		}
		if (Input.GetKey ("d")) {

			gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.right * 10);
		}
		if (Input.GetKey ("s")) {

			gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.back * 10);
		}
		if (Input.GetKeyDown (KeyCode.Space))
			gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
			

    }

}
