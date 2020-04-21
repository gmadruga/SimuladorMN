using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azimutal : propulsao
{

    void Awake()
    {
        propulsor = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Deslocamento();
    }

    void Deslocamento()
    {
        Ajustar_Giro();

        if (ligado)
            propulsor.AddRelativeForce( (Vector3.right * 1000f )*potencia_atual );

		velocidade_atual = Calcular_velocidade ();
    }

    void Ajustar_Giro()
    {
        Vector3 novoGiro = new Vector3(0, angulo_leme, 0);
        transform.localEulerAngles = novoGiro ;
    }
}
