using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Menu_Pause : menu
{
	public bool stop;
	public Button BotaoVoltar2;
	public GameObject Controle;

	void Start() {

		HideAll();
		stop = false;
		Cursor.visible = true;
	

		// =========SETAR BOTOES==========//
		BotaoJogar.onClick = new Button.ButtonClickedEvent();
		BotaoOpcoes.onClick = new Button.ButtonClickedEvent();
		BotaoSair.onClick = new Button.ButtonClickedEvent();
		BotaoVoltar.onClick = new Button.ButtonClickedEvent();
		BotaoVoltar2.onClick = new Button.ButtonClickedEvent();
		BotaoSalvarPref.onClick = new Button.ButtonClickedEvent();
		BotaoIniciar.onClick = new Button.ButtonClickedEvent();

		BotaoVoltar.onClick.AddListener(() => HideAll() );
		BotaoVoltar2.onClick.AddListener(() => HideAll() );
		BotaoOpcoes.onClick.AddListener(() => Opcoes(true));
		BotaoSair.onClick.AddListener(() => Sair());
		BotaoSalvarPref.onClick.AddListener(() => Recarregar() );
		BotaoIniciar.onClick.AddListener(() => Iniciar_Simulação());
	}

	private void Opcoes(bool ativarOP){
		
		BotaoVoltar.gameObject.SetActive (!ativarOP);
		BotaoOpcoes.gameObject.SetActive (!ativarOP);
		BotaoSair.gameObject.SetActive (!ativarOP);
		//
		BotaoVoltar2.gameObject.SetActive (ativarOP);
		textoVol.gameObject.SetActive (false);
		BarraVolume.gameObject.SetActive (false);
		CaixaModoJanela.gameObject.SetActive (false);
		Resolucoes.gameObject.SetActive (false);
		Qualidades.gameObject.SetActive (false);
		BotaoJogar.gameObject.SetActive (false);
		BotaoSalvarPref.gameObject.SetActive (ativarOP);
		Barcos.gameObject.SetActive(ativarOP);
		Provas.gameObject.SetActive(ativarOP);
		BotaoIniciar.gameObject.SetActive(false);
	}

    void Update()
    {
		if (Input.GetButtonDown ("Cancel"))
			Pause ();
    }

	void Pause()
	{
		Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f ;
		stop = !stop;

		if (stop) {
			Opcoes (false);
		}
		else{
			HideAll ();
		}

	}

	public void HideAll()
	{
		BotaoJogar.gameObject.SetActive (false);
		BotaoOpcoes.gameObject.SetActive (false);
		BotaoSair.gameObject.SetActive (false);
		textoVol.gameObject.SetActive (false);
		BarraVolume.gameObject.SetActive (false);
		CaixaModoJanela.gameObject.SetActive (false);
		Resolucoes.gameObject.SetActive (false);
		Qualidades.gameObject.SetActive (false);
		BotaoVoltar.gameObject.SetActive (false);
		BotaoSalvarPref.gameObject.SetActive (false);
		Barcos.gameObject.SetActive(false);
		Provas.gameObject.SetActive(false);
		BotaoIniciar.gameObject.SetActive (false);
		BotaoVoltar2.gameObject.SetActive (false);


		Time.timeScale = 1 ;
		stop = false;
	}

	private void Recarregar()
	{
		PlayerPrefs.SetInt("barco_escolhido", Barcos.value );
		PlayerPrefs.SetInt("prova_escolhida", Provas.value) ;
		Controle.GetComponent<Barco_Prova>().Desativar_Tudo ();
		Controle.GetComponent<Barco_Prova>().Start ();
	}

	private void Sair()
	{
		SceneManager.LoadScene ("Inicial",LoadSceneMode.Single);
		Destroy (gameObject);
	}
}
