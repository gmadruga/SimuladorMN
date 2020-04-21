using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leme : MonoBehaviour
{
    public Rigidbody Propulsor;
    private float _velocidade = 0;
    private Vector3 _lado;
    public float Angulo_Leme;
    private Propulsao propulsao;

    // Não está realista apesar de estar virando. Consertar!!
    // Falta também colocar script do angulo e consertar a questão da velocidade
    private void Start()
    {
        propulsao = Propulsor.GetComponent<Propulsao>();
    }

    void Update()
    {
        float eixoZ_Lado = Input.GetAxis(Tags.Horizontal);
        _lado = new Vector3(0, eixoZ_Lado, 0);
       
        _velocidade = propulsao.Velocidade / propulsao.VelocidadeMaxPotenciaMax ;

    }

    private void FixedUpdate()
    {
        if(_lado.magnitude != 0)
        {
            VirarLeme();
        }     
    }

    private void VirarLeme()
    {
        bool IndoParaTras = Propulsor.GetComponent<Propulsao>()._frente.x < 0;

        if (IndoParaTras)
            Propulsor.AddRelativeTorque(-_lado* Mathf.Pow(_velocidade, 4) * Angulo_Leme * 1000f);
        else
            Propulsor.AddRelativeTorque(_lado * Mathf.Pow(_velocidade, 4) * Angulo_Leme * 1000f);
    }
}
