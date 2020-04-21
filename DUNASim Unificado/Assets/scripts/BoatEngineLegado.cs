using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoatEngineLegado : MonoBehaviour 
{
	//Drags
	public Transform waterJetTransform;
	public Transform leftThrusterTransform;
	public Transform rightThrusterTransform;
	public Transform leme1;
	public Transform leme2;
	//How fast should the engine accelerate?
	//public float powerFactor;

	//What's the boat's maximum engine power?
	public float maxPower;
	public float thrusterPower;

	//The boat's current engine power is public for debugging
	public float currentJetPower;

	private float currentLeftThrusterPower;
	private float currentRightThrusterPower;

	private float thrustFromWaterJet = 0f;

	private Rigidbody boatRB;

	private float WaterJetRotation_Y = 0f;

	BoatController boatController;

	void Start() 
	{
		boatRB = GetComponent<Rigidbody>();

		boatController = GetComponent<BoatController>();
	}


	void Update() 
	{
		UserInput();
	}

	void FixedUpdate()
	{
		UpdateWaterJet();
	}

	void UserInput()
	{
		//Analogico Esquerdo
		var vLeft = Input.GetAxis("VerticalLeft");
		var hLeft = Input.GetAxis("HorizontalLeft");
		//Analogico Direito
		var vRight = Input.GetAxis("VerticalRight");
		var hRight = Input.GetAxis("HorizontalRight");
		//Trigger Direito (R2/RT)
		//var trigger = Input.GetAxis ("Trigger");
		//var ltrigger = Input.GetAxis ("LeftTrigger");
		//Butoes Traseiros (R1/L1 ou RB/LB)
		var leftButton = Input.GetButton("LeftButton");
		var rightButton = Input.GetButton("RightButton");

		//hLeft = AnalogicoEsquerdo:
		//Para Cima -> hLeft = -1
		//Para Baixo -> hLeft = +1

		if (vRight > 0) {
			//Fator Multiplicativo para Re
			currentJetPower = maxPower * -vRight * 0.6f;
		}

		else {
			if (vRight < 0) {
				currentJetPower = maxPower * -vRight;

			} 

			else {
				currentJetPower = 0f;
			}
		}

		//Jet Thrusters

		if (leftButton) {
			currentLeftThrusterPower = thrusterPower;
		}

		else {
			if (rightButton) {
				currentRightThrusterPower = thrusterPower;
			} 

			else {
				currentLeftThrusterPower = 0f;
				currentRightThrusterPower = 0f;
			}
				
		}


		/*
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//DESCOMENTA PARA UTILIZAR TECLADO
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

		//Forward / reverse

		if (Input.GetKey(KeyCode.W))
		{
				currentJetPower = maxPower;
		}
		else
		{
			if (Input.GetKey (KeyCode.S)) {
				currentJetPower = -maxPower * 0.6f;
			} else {
				currentJetPower = 0f;
			}
		}


		//Steer left
		if (Input.GetKey(KeyCode.A))
		{
			//WaterJetRotation_Y = waterJetTransform.localEulerAngles.y + 3f;
			WaterJetRotation_Y = 30f + 90f;

			//if (WaterJetRotation_Y > 50f && WaterJetRotation_Y < 250f)
			if (WaterJetRotation_Y > 140f)
			{
				//WaterJetRotation_Y = 50f;
				WaterJetRotation_Y = 140f;
			}

			//Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);

			//waterJetTransform.localEulerAngles = newRotation;
		}
		//Steer right
		else if (Input.GetKey(KeyCode.D))
		{
			//WaterJetRotation_Y = waterJetTransform.localEulerAngles.y - 3f;
			WaterJetRotation_Y = -30f + 90f;

			//if (WaterJetRotation_Y < 310f && WaterJetRotation_Y > 70f)
			if (WaterJetRotation_Y < 40f)
			{
				//WaterJetRotation_Y = 310f;
				WaterJetRotation_Y = 40f;
			}

			//Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);

			//waterJetTransform.localEulerAngles = newRotation;
		}

		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		*/

		// -1 < hRight < 1
		WaterJetRotation_Y = -hLeft*20f + 90f;

		Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);
		Vector3 rotationLeme = new Vector3 (0f, WaterJetRotation_Y, 0f);

		waterJetTransform.localEulerAngles = newRotation;
		leme1.localEulerAngles = rotationLeme;
		leme2.localEulerAngles = rotationLeme;

	}

	void UpdateWaterJet()
	{
		//Debug.Log(boatController.CurrentSpeed);

		Vector3 forceToAdd = -waterJetTransform.forward * currentJetPower;
		Vector3 forceToAddLeftThruster = -leftThrusterTransform.forward * currentLeftThrusterPower;
		Vector3 forceToAddRightThruster = rightThrusterTransform.forward * currentRightThrusterPower;
		//Vector3 forceToAdd = -waterJetTransform.forward * 300f;

		//Only add the force if the engine is below sea level
		//float waveYPos = WaterController.current.GetWaveYPos(waterJetTransform.position, Time.time);

		boatRB.AddForceAtPosition(forceToAdd, waterJetTransform.position);
		boatRB.AddForceAtPosition(forceToAddLeftThruster, leftThrusterTransform.position);
		boatRB.AddForceAtPosition(forceToAddRightThruster, rightThrusterTransform.position);


	}
}