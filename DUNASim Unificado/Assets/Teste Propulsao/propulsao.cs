using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class propulsao : MonoBehaviour
{
	public float potencia_atual, angulo_leme;
	//public float potencia_maxima = 8;
	public bool ligado;
	public float velocidade_atual = 0;
	Vector3 lastposition;
	public float tamanhoBarco = 3.7f;

	[HideInInspector]
	public Rigidbody propulsor; 


	const float BARCO_OCUPA_QUADRADOS = 3.7f;

    void Awake()
    {
		propulsor = gameObject.GetComponent<Rigidbody> ();
    }

	void Start()
	{
		lastposition = transform.position;
	}

	void FixedUpdate()
    {
		if (ligado)
			Deslocamento ();

		velocidade_atual = Calcular_velocidade ();
    }

	void Deslocamento()
	{


		propulsor.AddRelativeForce (Vector3.right * potencia_atual * 1000f);
		propulsor.AddRelativeTorque (Vector3.up * potencia_atual * angulo_leme * 100f); 
	}

	public float Calcular_velocidade ()
	{
		
		float deslocamento = (transform.position - lastposition).magnitude;
		lastposition = transform.position;

		// q/s * m/q = m/s
		float velocidade =  (deslocamento / Time.deltaTime) * (tamanhoBarco/BARCO_OCUPA_QUADRADOS);
		return velocidade ;
	}
}
