using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassouBoia : MonoBehaviour
{
	private Renderer BoiaRenderer;
    public GameObject LinhaDeChegada;
    private LapScript _scriptLap;
	public bool Ignora_Player, ultrapassada;
    public Material red, green;

    // Start is called before the first frame update
    void Start()
	{
		BoiaRenderer = GetComponent<Renderer>();
		BoiaRenderer.material = red;
        _scriptLap = LinhaDeChegada.GetComponent<LapScript>();
		ultrapassada = false;
	}

    private void OnEnable()
    {
        BoiaRenderer.material = red;
        ultrapassada = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
		if (_scriptLap.AllFloaterRed) {

			BoiaRenderer.material = red;
			ultrapassada = false;
		}
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
	{
		if (Ignora_Player && objetoDeColisao.tag == "Player")
			return;

        if (objetoDeColisao.tag == "Player" || objetoDeColisao.tag == "Barcaca")
        {
            _scriptLap.AllFloaterRed = false;
            BoiaRenderer.material = green;
            ultrapassada = true;
        }

	}
}
