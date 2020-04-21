using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    private bool _clockCamera = false;
    public Camera CameraDoScript;
    private Vector3 _posicaoInicial,_posicaoRelativaBarco;
    public bool CameraSegueJogador = false, automatico = false;
    public GameObject _barcoAtual;
    private float _quantoLateralmente;
    private float _quantoVerticalmente;
    private float MoveCameraLateralmente, MoveCameraVerticalmente;

    void Start()
	{
        GameObject controlador = GameObject.FindGameObjectWithTag("GameController");
        _barcoAtual = Barco_Prova.barco_atual ;
        _posicaoInicial = transform.position;
        _posicaoRelativaBarco = transform.position - _barcoAtual.transform.position;

        _quantoLateralmente = transform.rotation.eulerAngles.y;
        _quantoVerticalmente = transform.rotation.eulerAngles.x;
    }

    private void FixedUpdate()
    {
        ModoDaCamera(_barcoAtual, CameraSegueJogador);

    }

    // Update is called once per frame
    void Update()
	{
        // Para salvar a posição relativa uma vez só quando selecionar
        if (automatico) // PODE MELHORAR
        {
            transform.LookAt(Barco_Prova.barco_atual.transform.position + (Vector3.left * 4) , Vector3.up);
            return;
        }

        if (CameraSegueJogador)
        {
            if (_clockCamera == true)
            {
                transform.position = _barcoAtual.transform.position + _posicaoRelativaBarco;
                _clockCamera = !_clockCamera;
            }
        }
        else
        {
            _clockCamera = true;
        }

        MoveCameraLateralmente = Input.GetAxis("Horizontal2");
        MoveCameraVerticalmente = Input.GetAxis("Vertical2");

        _quantoLateralmente += MoveCameraLateralmente * 4;
        _quantoVerticalmente += MoveCameraVerticalmente * 4;

        Vector3 novoGiro = new Vector3(_quantoVerticalmente, _quantoLateralmente, 0);
        transform.localEulerAngles = novoGiro;

    }

    public void ModoDaCamera(GameObject barco, bool segueJogador)
    {
        if (segueJogador)
        {
            this.transform.SetParent(barco.transform);
            //transform.position = _barcoAtual.transform.position + _posicaoRelativaBarco;
        }
        else
        {
            DetachFromParent(CameraDoScript);
            transform.position = _posicaoInicial;
        }
    }

    public void DetachFromParent(Camera objeto)
    {
        objeto.transform.parent = null;
    }
}




/*public GameObject BarcoProp, BarcoAzimutal;
	private Vector3 _distCompensar;
	public GameObject ReferenciaAzimutal, ReferenciaProp;
	private Vector3 _ajeitaCentroAzimutal, _ajeitaCentroProp;
	private float _anguloAziCam, _anguloPropCam;
	*/




/*
       _ajeitaCentroAzimutal = ReferenciaAzimutal.transform.position;
       _ajeitaCentroProp = ReferenciaProp.transform.position;
        //Pega a posição da câmera em relação ao (0,0,0) do mundo e diminui pela posição do jogador
        //também em relação ao (0,0,0) do mundo. Exemplo: (20,35,60) - (15, 35, 50) = (5,0,10)
        //Ficando nessa distância com relação ao jogador sempre
       if( BarcoProp.activeSelf )
           _distCompensar = transform.position - (BarcoProp.transform.position + 
               (_ajeitaCentroProp - BarcoProp.transform.position));
       else
           _distCompensar = transform.position - (BarcoAzimutal.transform.position + 
               (_ajeitaCentroAzimutal - BarcoAzimutal.transform.position));
       */
