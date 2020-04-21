using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


// Por enquanto só no teclado funciona a propulsão
public class Propulsao : MonoBehaviour
{
    private Rigidbody _propulsor;
    public Vector3 _frente { get; private set; }

    /* ~~~~~~~~~~~~~~~~ Tempos ~~~~~~~~~~~~~~~~ */

    public float TempVelMaxPotMax = 2f;
    private readonly short GambiarraTempo = 2; // Gambiarra para o tempo bater

    public float TempoParaVelocidadeMaxAtual { get; private set; }
    private void SetTempoParaVelocidadeMaxAtual()
    {
        TempoParaVelocidadeMaxAtual = TempVelMaxPotMax * Potencia_Atual ;
    }

    // Apenas para não ir para a velocidade máxima de uma vez
    private float _tempoAcelerando = 0;
    public float GetTempoAcelerando()
    {
        float tempoAuxiliar = _tempoAcelerando;

        if (tempoAuxiliar < 0)
            tempoAuxiliar *= -1;

        return tempoAuxiliar;
    }

    private float SetTempoAcelerando (Vector3 frenteOuTras)
    {
        if (frenteOuTras.magnitude != 0)
        {
            if (frenteOuTras.x > 0.7 && _tempoAcelerando < TempoParaVelocidadeMaxAtual)
            {
                if (_tempoAcelerando < 0)
                    _tempoAcelerando = 0;
                // Para que chegue na velocidade maxima da potencia sempre no mesmo tempo
                // metade da potencia : 1/2 do tempo da potMax e deltaTime vai crescer pela metade
                _tempoAcelerando += Time.deltaTime * (TempoParaVelocidadeMaxAtual * GambiarraTempo / TempVelMaxPotMax);
            }
            else if (frenteOuTras.x > 0.7 && _tempoAcelerando > TempoParaVelocidadeMaxAtual)
                _tempoAcelerando = TempoParaVelocidadeMaxAtual;

            else if (frenteOuTras.x < -0.7 && _tempoAcelerando > -TempoParaVelocidadeMaxAtual)
            {
                if (_tempoAcelerando > 0)
                    _tempoAcelerando = 0;
                _tempoAcelerando -= Time.deltaTime * (TempoParaVelocidadeMaxAtual * GambiarraTempo / TempVelMaxPotMax);
            }
            else if(frenteOuTras.x < -0.7 && _tempoAcelerando < -TempoParaVelocidadeMaxAtual)
                _tempoAcelerando = TempoParaVelocidadeMaxAtual;
        }
        else // Apertando nada
        {
            if (_tempoAcelerando < 0)
            {
                _tempoAcelerando += Time.deltaTime * 1.1f;
            }
            else
            {
                _tempoAcelerando -= Time.deltaTime * 1.1f;
            }

        }
       
        return _tempoAcelerando;
    }


    /* ~~~~~~~~~~~~~~~~ Potencia e Velocidade ~~~~~~~~~~~~~~~~ */


    public float Potencia_Atual { get; private set; }
    private void SetPotenciaAtual_Teclado (Vector3 frenteOuTras)
    {
        // Antigo código usando potencia como velocidade:
        // Potencia_Atual = (tempo / tempoParaVelocidadeMaxima) * Potencia_Maxima;
        if(frenteOuTras.x != 0)
        {
            Potencia_Atual = 1f;
        }
        else
            Potencia_Atual = 0f; 
    }

    // Como com o controle tem o analógico e não é somento 0 e 1, é outra lógica
    /*
    private void SetPotenciaAtual_Controle(Vector3 frenteOuTras)
    {
        Potencia_Atual = (tempo / tempoParaVelocidadeMaxima) * Potencia_Maxima;
        if (frenteOuTras.x != 0)
        {
            Potencia_Atual = 1f;
        }
    }
    */

    public float VelocidadeMaxPotenciaMax = 30f;
    public float VelocidadeMaxAtual { get; private set; }   
    private void SetVelocidadeMaxAtual()
    {
        VelocidadeMaxAtual = VelocidadeMaxPotenciaMax * Potencia_Atual;
    }

    public float Velocidade { get; private set; }
    private void SetVelocidade()
    {
        if (TempoParaVelocidadeMaxAtual == 0)
          Velocidade = 0;
        else
            Velocidade = (GetTempoAcelerando() / TempoParaVelocidadeMaxAtual) * VelocidadeMaxAtual;


        /*
        try
        {
            Velocidade = (GetTempoAcelerando() / TempoParaVelocidadeMaxAtual) * VelocidadeMaxAtual;
        }
        catch (DivideByZeroException)
        {
            Velocidade = 0;
        }
        */
    }

    private short _ajusteVelocidade = 600;
    public short GetAjusteVelocidade()
    {
        return _ajusteVelocidade;
    }

    /* ~~~~~~~~~~~~~~~~~~~~~~  Programa ~~~~~~~~~~~~~~~~~~~~~~ */


    private void Awake()
    {
        _propulsor = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
         float eixoX_Frente = Input.GetAxis(Tags.Vertical);
        _frente = new Vector3(eixoX_Frente,0,0);

        // se controle teclado:
        SetPotenciaAtual_Teclado(_frente);
        SetTempoParaVelocidadeMaxAtual();
        SetTempoAcelerando(_frente);
        SetVelocidadeMaxAtual();
        SetVelocidade();
    }

    private void FixedUpdate()
    {
        if(_frente.magnitude != 0)
            PropulsaoNoMotor();
    }


    /* ~~~~~~~~~~~~~~~~~~~~~~  Métodos ~~~~~~~~~~~~~~~~~~~~~~ */


    private void PropulsaoNoMotor ()
    { 
        _propulsor.AddRelativeForce(_frente * Velocidade * _ajusteVelocidade);
    }

}
