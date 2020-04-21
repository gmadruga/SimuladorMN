using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class BoatEngine : MonoBehaviour 
{
	//Drags
	public Transform motor;
	public Transform motorEsquerdo;
	public Transform motorDireito;
	//public Transform leftThrusterTransform;
	//public Transform rightThrusterTransform;
	public Transform leme1;
	public Transform leme2;
	//How fast should the engine accelerate?
	//public float powerFactor;

	//What's the boat's maximum engine power?
	public float maxPower;
	public float factorTeclado = 200f;
	//public float thrusterPower;

	//The boat's current engine power is public for debugging
	public float currentJetPower;

	public float motorEsquerdoPower;
	public float motorDireitoPower;

	//private float currentLeftThrusterPower;
	//private float currentRightThrusterPower;

	private float thrustFromWaterJet = 0f;
	private Rigidbody boatRB;
	private float WaterJetRotation_Y = 0f;
	BoatController boatController;

	//false = Motores Dependentes
	//true = Motores Independentes
	public bool opcaoControle;
	public Button controle; 
	private Button btn1;

	void Start() 
	{
		boatRB = GetComponent<Rigidbody>();

		boatController = GetComponent<BoatController>();

		btn1 = controle.GetComponent<Button>();
		btn1.onClick.AddListener(TaskOnClick);

		WaterJetRotation_Y = 90f;

		leme1.localEulerAngles = Vector3.zero + Vector3.forward * 90;
		leme2.localEulerAngles = Vector3.zero + Vector3.forward * 90;
		motor.localEulerAngles = Vector3.zero + Vector3.forward * 90;
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
		//Analogico Esquerdo Vertical
		var vLeft = Input.GetAxis("VerticalLeft");
		//Analogico Esquerdo Horizontal
		var hLeft = Input.GetAxis("HorizontalLeft");
		//Analogico Direito Horizontal
		var hRight  = Input.GetAxis("HorizontalRight");
		//Analogico Direito Vertical
		var vRight = Input.GetAxis("VerticalRight");
		//Trigger Direito (R2/RT)
		//var trigger = Input.GetAxis ("Trigger");
		//var ltrigger = Input.GetAxis ("LeftTrigger");
		//Butoes Traseiros (R1/L1 ou RB/LB)
		var rightButton = Input.GetButton("LeftButton");
		var leftButton = Input.GetButton("RightButton");

		//Para Cima -> vLeft = -1
		//Para Baixo -> vLeft = +1

		if (opcaoControle) {
			vLeft = vLeft / 2;
			vRight = vRight / 2;
			if (vLeft > 0) {
				motorEsquerdoPower = maxPower * -vLeft * 0.6f;
			} else {
				if (vLeft < 0) {
					motorEsquerdoPower = maxPower * -vLeft;
				} else {
					motorEsquerdoPower = 0f;
				}

			}

			if (vRight > 0) {
				motorDireitoPower = maxPower * -vRight * 0.6f;
			} else {
				if (vRight < 0) {
					motorDireitoPower = maxPower * -vRight;
				} else {
					motorDireitoPower = 0f;
				}
			}
		} else {
			if (vRight > 0) {
				//Fator Multiplicativo para Re
				currentJetPower = maxPower * -vRight * 0.6f;
			} else {
				if (vRight < 0) {
					currentJetPower = maxPower * -vRight;

				} else {
					currentJetPower = 0f;
				}
			}
		}

		//Jet Thrusters (Legado)
		/*
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
		*/

		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//DESCOMENTA PARA UTILIZAR TECLADO
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

		//*
		 
		//Forward / reverse
		if (Input.GetKey(KeyCode.W))
		{
			if (currentJetPower <= maxPower)
				currentJetPower += factorTeclado;
		}
		else
		{
			if (Input.GetKey (KeyCode.S)) {
				if (currentJetPower >= -maxPower)
				currentJetPower -= factorTeclado * 0.6f;
			} else {
				currentJetPower = 0f;
			}
		}


		//Steer left
		if (Input.GetKey(KeyCode.A))
		{
			WaterJetRotation_Y = motor.localEulerAngles.y + 2f;
			//WaterJetRotation_Y = 30f + 90f;

			//if (WaterJetRotation_Y > 50f && WaterJetRotation_Y < 250f)
			if (WaterJetRotation_Y > 120f)
			{
				//WaterJetRotation_Y = 50f;
				WaterJetRotation_Y = 120f;
			}

			//Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);

			//waterJetTransform.localEulerAngles = newRotation;
		}
		//Steer right
		else if (Input.GetKey(KeyCode.D))
		{
			WaterJetRotation_Y = motor.localEulerAngles.y - 2f;
			//WaterJetRotation_Y = -30f + 90f;

			//if (WaterJetRotation_Y < 310f && WaterJetRotation_Y > 70f)
			if (WaterJetRotation_Y < 60f)
			{
				//WaterJetRotation_Y = 310f;
				WaterJetRotation_Y = 60f;
			}

			//Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);

			//waterJetTransform.localEulerAngles = newRotation;
		}

		//*/

		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//COMENTA PARA TECLADO
		//DESCOMENTA PARA CONTROLE

		//WaterJetRotation_Y = -hLeft*30f + 90f;

		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

		Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);
		Vector3 rotationLeme = new Vector3 (0f, WaterJetRotation_Y + 90f, 0f);

		motor.localEulerAngles = newRotation;
		leme1.localEulerAngles = rotationLeme;
		leme2.localEulerAngles = rotationLeme;

	}

	void UpdateWaterJet()
	{
		//Debug.Log(boatController.CurrentSpeed);

		Vector3 forceToAdd = -motor.forward * currentJetPower;
		Vector3 forceToAddMotorE = -motorEsquerdo.right * motorEsquerdoPower;
		Vector3 forceToAddMotorD = -motorDireito.right * motorDireitoPower;

		//Vector3 forceToAddLeftThruster = -leftThrusterTransform.forward * currentLeftThrusterPower;
		//Vector3 forceToAddRightThruster = rightThrusterTransform.forward * currentRightThrusterPower;
		//Vector3 forceToAdd = -waterJetTransform.forward * 300f;

		//Only add the force if the engine is below sea level
		//float waveYPos = WaterController.current.GetWaveYPos(waterJetTransform.position, Time.time);

		boatRB.AddForceAtPosition(forceToAdd, motor.position);
		boatRB.AddForceAtPosition (forceToAddMotorE, motorEsquerdo.position);
		boatRB.AddForceAtPosition (forceToAddMotorD, motorDireito.position);
		//boatRB.AddForceAtPosition(forceToAddLeftThruster, leftThrusterTransform.position);
		//boatRB.AddForceAtPosition(forceToAddRightThruster, rightThrusterTransform.position);


	}

	void TaskOnClick(){
			opcaoControle = !opcaoControle;
	}
}