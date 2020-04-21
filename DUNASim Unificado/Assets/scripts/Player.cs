using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO.Ports;

public class Player : MonoBehaviour
{
	public Transform vela;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		var h1 = Input.GetAxis("Horizontal");
		var h2 = Input.GetAxis("Horizontal2");

	
		transform.Rotate(0, h1, 0);
		vela.transform.Rotate (0, h2, 0);
	}
}