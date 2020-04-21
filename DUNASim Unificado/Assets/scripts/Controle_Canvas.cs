using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle_Canvas : MonoBehaviour {

	public GameObject canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Tab))
		{
			if (canvas.activeSelf)
				canvas.SetActive(false);
			else
				canvas.SetActive(true);
		}
	}
}
