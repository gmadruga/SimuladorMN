using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcularMovimento : MonoBehaviour
{
    public GameObject Propulsao; // Caso não coloque o script na propulsao em si
    private int _ajusteVelocidade;
    public float Velocidade_km_h = 0;
    private Vector3 _lastposition;


    private void Start()
    {
        // Só funciona para AjusteVelocidade (propulsao) = 600. Manter isso certo porque é usado em outro script
        _ajusteVelocidade = Propulsao.GetComponent<Propulsao>().GetAjusteVelocidade() / 60;
        _lastposition = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Velocidade_km_h = Calcular_velocidade(); 
    }

    float Calcular_velocidade()
    {

        float deslocamento = (transform.position - _lastposition).magnitude;
        _lastposition = transform.position;

        // quadrados / segundos * metros/quadrados = m/s
        float velocidade = (deslocamento / Time.deltaTime) * _ajusteVelocidade;
        return velocidade;
    }
}
