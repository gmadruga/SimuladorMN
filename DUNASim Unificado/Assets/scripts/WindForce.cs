using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{	
	private Rigidbody boatRB;
	public Transform vela;
	public Transform barco;

	void Awake ()
	{
		boatRB = this.GetComponent<Rigidbody> ();
	}


	void OnTriggerStay (Collider other)
	{
		
		if (other.tag == "Wind") 
		{
			float angulo;
			float anguloResultante;
			float anguloNavio;

			angulo = other.transform.eulerAngles.y;   // Direção do vento, ou seja, direção para onde o navio deve ser empurrado
			anguloResultante = vela.transform.eulerAngles.y - other.transform.eulerAngles.y;  // Direfença entre a direção do vento e a da vela, ou seja, intensidade de força que o vento vai empurrar o navio
			anguloNavio = barco.eulerAngles.y;

		//	Debug.Log ("Angulo do Vento: " + angulo);
		//	Debug.Log ("Angulo Resultante: " + anguloResultante);
		//	Debug.Log (vela.transform.eulerAngles.y);

			angulo = angulo * Mathf.PI / 180f;
			anguloResultante = anguloResultante * Mathf.PI / 180f;



			Vector3 forceCasco;
			forceCasco.x = (other.GetComponent<WindZone> ().windMain + other.GetComponent<TurbulenceWind>().Turbulence) * Mathf.Sin(angulo);   // A força no casco sempre empurra na direção do vento
			forceCasco.y = 0;
			forceCasco.z = (other.GetComponent<WindZone> ().windMain + other.GetComponent<TurbulenceWind>().Turbulence) * Mathf.Cos(angulo);

			boatRB.AddForce (forceCasco*0.1f);

			Vector3 forceVela;
			forceVela.x = -(other.GetComponent<WindZone> ().windMain + other.GetComponent<TurbulenceWind>().Turbulence) * Mathf.Abs( Mathf.Cos(anguloResultante));
			forceVela.y = 0;
			forceVela.z = 0;

			if (Vector3.Angle (barco.right, other.transform.forward) < 90 && Vector3.Angle (barco.right, other.transform.forward) > 30)
				forceVela.x *= Mathf.Abs(Mathf.Sin(Vector3.Angle (barco.right, other.transform.forward))) * 1.1f;

			if (Vector3.Angle (barco.right, other.transform.forward) < 30)
				forceVela.x = 0;

			Vector3 resultante = forceVela.x * barco.right;

			Debug.Log (boatRB.velocity);
			// A força na vela vai empurrar na direção do vento, porém
			// Apenas se a vela estiver na direção do vento, ou seja, anguloResultante define a intensidade dessa força
			boatRB.AddForce (resultante);
		}
	}
		

}