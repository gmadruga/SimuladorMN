using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// integra o sistema de propulsão azimutal do barco 2 ao Input

public class ControleAzimutal : MonoBehaviour
{

    public Azimutal azimutal;
    public float x_axis, y_axis; // mede a intensidade no comando "analógico"

    public float potencia_maxima;
    public float angulo0 = 15 ; // ângulações pré definidas
    public float angulo1 = 30;
    public float angulo2 = 45;
    void Start()
    {
		//azimutal = gameObject.GetComponent<Azimutal> ();
    }

    // Update is called once per frame
    void Update()
    {
		InputUser ();
    }

	private void InputUser()
	{

		x_axis = Input.GetAxis (Tags.Vertical); 
		y_axis = Input.GetAxis(Tags.Horizontal); 

		float incremento_potencia;

		// se a potencia atual for maior que o máximo, não haverá incremento no valor 
		incremento_potencia = (Mathf.Abs (azimutal.potencia_atual) < potencia_maxima) ?
			x_axis : 0;

		azimutal.potencia_atual = (x_axis == 0) ? 0 : azimutal.potencia_atual;
		azimutal.potencia_atual += incremento_potencia;

		float angulo_base; // angulo para o qual a helice do azimutal apontará em uma curva

		// "control" e "space" são usados para fazer giros mais rápidos 

		if (Input.GetKey (KeyCode.LeftControl))
			angulo_base = angulo1 ;
		else if (Input.GetKey (KeyCode.Space))
			angulo_base = angulo2;

		else
			angulo_base = angulo0;

		azimutal.angulo_leme = 180 + (-y_axis * angulo_base) ;
	}
}

