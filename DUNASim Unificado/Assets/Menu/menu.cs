using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

// Controla o menu inicial, suas propriedades e interações 

public class menu : MonoBehaviour {

	public GameObject camera;
	public Button BotaoJogar,BotaoOpcoes,BotaoSair;
	[Space(20)]
	public Slider BarraVolume;
	public Toggle CaixaModoJanela;
	public Dropdown Resolucoes, Qualidades, Barcos, Provas;
	public Button BotaoVoltar, BotaoSalvarPref, BotaoIniciar;
	[Space(20)]
	public Text textoVol;
	public string nomeCenaJogo = "barcos_duna_2019";
	public string nomeDaCena;
	public float VOLUME;
	public int qualidadeGrafica, modoJanelaAtivo, resolucaoSalveIndex;
	public bool telaCheiaAtivada;
	public Resolution[] resolucoesSuportadas;

	public void Awake(){
		DontDestroyOnLoad (transform.gameObject);
		resolucoesSuportadas = Screen.resolutions;
	}

	public void Start() {
        Opcoes(false);

        ChecarResolucoes();
        AjustarQualidades();
        //
        if (PlayerPrefs.HasKey("RESOLUCAO")) {
            int numResoluc = PlayerPrefs.GetInt("RESOLUCAO");
            if (resolucoesSuportadas.Length <= numResoluc) {
                PlayerPrefs.DeleteKey("RESOLUCAO");
            }
        }
        //
        nomeDaCena = SceneManager.GetActiveScene().name;
        Cursor.visible = true;
        Time.timeScale = 1;
        //
        BarraVolume.minValue = 0;
        BarraVolume.maxValue = 1;

        //=============== SAVES===========//
        if (PlayerPrefs.HasKey("VOLUME")) {
            VOLUME = PlayerPrefs.GetFloat("VOLUME");
            BarraVolume.value = VOLUME;
        } else {
            PlayerPrefs.SetFloat("VOLUME", 1);
            BarraVolume.value = 1;
        }
        //=============MODO JANELA===========//
        if (PlayerPrefs.HasKey("modoJanela")) {
            modoJanelaAtivo = PlayerPrefs.GetInt("modoJanela");
            if (modoJanelaAtivo == 1) {
                Screen.fullScreen = false;
                CaixaModoJanela.isOn = true;
            } else {
                Screen.fullScreen = true;
                CaixaModoJanela.isOn = false;
            }
        } else {
            modoJanelaAtivo = 0;
            PlayerPrefs.SetInt("modoJanela", modoJanelaAtivo);
            CaixaModoJanela.isOn = false;
            Screen.fullScreen = true;

        }
        //========RESOLUCOES========//
        if (modoJanelaAtivo == 1) {
            telaCheiaAtivada = false;
        } else {
            telaCheiaAtivada = true;
        }
        if (PlayerPrefs.HasKey("RESOLUCAO")) {
            resolucaoSalveIndex = PlayerPrefs.GetInt("RESOLUCAO");
            Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width, resolucoesSuportadas[resolucaoSalveIndex].height, telaCheiaAtivada);
            Resolucoes.value = resolucaoSalveIndex;
        } else {
            resolucaoSalveIndex = (resolucoesSuportadas.Length - 1);
            Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width, resolucoesSuportadas[resolucaoSalveIndex].height, telaCheiaAtivada);
            PlayerPrefs.SetInt("RESOLUCAO", resolucaoSalveIndex);
            Resolucoes.value = resolucaoSalveIndex;
        }
        //=========QUALIDADES=========//
        if (PlayerPrefs.HasKey("qualidadeGrafica")) {
            qualidadeGrafica = PlayerPrefs.GetInt("qualidadeGrafica");
            QualitySettings.SetQualityLevel(qualidadeGrafica);
            Qualidades.value = qualidadeGrafica;
        } else {
            QualitySettings.SetQualityLevel((QualitySettings.names.Length - 1));
            qualidadeGrafica = (QualitySettings.names.Length - 1);
            PlayerPrefs.SetInt("qualidadeGrafica", qualidadeGrafica);
            Qualidades.value = qualidadeGrafica;
        }

        // =========SETAR BOTOES==========//
        BotaoJogar.onClick = new Button.ButtonClickedEvent();
        BotaoOpcoes.onClick = new Button.ButtonClickedEvent();
        BotaoSair.onClick = new Button.ButtonClickedEvent();
        BotaoVoltar.onClick = new Button.ButtonClickedEvent();
        BotaoSalvarPref.onClick = new Button.ButtonClickedEvent();
        BotaoIniciar.onClick = new Button.ButtonClickedEvent();


        BotaoJogar.onClick.AddListener(() => Jogar());
        BotaoOpcoes.onClick.AddListener(() => Opcoes(true));
        BotaoSair.onClick.AddListener(() => Sair());
        BotaoVoltar.onClick.AddListener(() => Opcoes(false));
        BotaoSalvarPref.onClick.AddListener(() => SalvarPreferencias());
        BotaoIniciar.onClick.AddListener(() => Iniciar_Simulação());

        Barcos.onValueChanged.AddListener(delegate { gameObject.GetComponent<ExibirBarco>().Show(Barcos); });
        BotaoVoltar.onClick.AddListener(delegate { gameObject.GetComponent<ExibirBarco>().end();  });
    }

	//=========MÉTODOS DE CHECAGEM==========//
	public void ChecarResolucoes(){
		Resolution[] resolucoesSuportadas = Screen.resolutions;
		Resolucoes.options.Clear ();
		for(int y = 0; y < resolucoesSuportadas.Length; y++){
			Resolucoes.options.Add(new Dropdown.OptionData() { text = resolucoesSuportadas[y].width + "x" + resolucoesSuportadas[y].height });
		}
		Resolucoes.captionText.text = "Resolucao";
	}
	public void AjustarQualidades(){
		string[] nomes = QualitySettings.names;
		Qualidades.options.Clear ();
		for(int y = 0; y < nomes.Length; y++){
			Qualidades.options.Add(new Dropdown.OptionData() { text = nomes[y] });
		}
		Qualidades.captionText.text = "Qualidade";
	}
	public void Opcoes(bool ativarOP){
		BotaoJogar.gameObject.SetActive (!ativarOP);
		BotaoOpcoes.gameObject.SetActive (!ativarOP);
		BotaoSair.gameObject.SetActive (!ativarOP);
		//
		textoVol.gameObject.SetActive (ativarOP);
		BarraVolume.gameObject.SetActive (ativarOP);
		CaixaModoJanela.gameObject.SetActive (ativarOP);
		Resolucoes.gameObject.SetActive (ativarOP);
		Qualidades.gameObject.SetActive (ativarOP);
		BotaoVoltar.gameObject.SetActive (ativarOP);
		BotaoSalvarPref.gameObject.SetActive (ativarOP);
        Barcos.gameObject.SetActive(false);
        Provas.gameObject.SetActive(false);
        BotaoIniciar.gameObject.SetActive(false);
    }

	//=========MÉTODOS DE SALVAMENTO==========//

	public void SalvarPreferencias(){
		if (CaixaModoJanela.isOn == true) {
			modoJanelaAtivo = 1;
			telaCheiaAtivada = false;
		} else {
			modoJanelaAtivo = 0;
			telaCheiaAtivada = true;
		}
		PlayerPrefs.SetFloat ("VOLUME", BarraVolume.value);
		PlayerPrefs.SetInt ("qualidadeGrafica", Qualidades.value);
		PlayerPrefs.SetInt ("modoJanela", modoJanelaAtivo);
		PlayerPrefs.SetInt ("RESOLUCAO", Resolucoes.value);
		resolucaoSalveIndex = Resolucoes.value;
		AplicarPreferencias ();
	}
	public void AplicarPreferencias(){
		VOLUME = PlayerPrefs.GetFloat ("VOLUME");
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt ("qualidadeGrafica"));
		Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width,resolucoesSuportadas[resolucaoSalveIndex].height,telaCheiaAtivada);
	}

	//===========OUTROS MÉTODOS=========//

	public void Update(){
		if (SceneManager.GetActiveScene ().name != nomeDaCena) {
			AudioListener.volume = VOLUME;
			Destroy (gameObject);
		}
	}
	public void Jogar(){

		Modos_de_Jogo ();
	}

	public void Modos_de_Jogo()
	{
		// desativar os elementos dos menus anteriores
		{
		    BotaoJogar.gameObject.SetActive (false);
			BotaoOpcoes.gameObject.SetActive (false);
			BotaoSair.gameObject.SetActive (false);
			textoVol.gameObject.SetActive (false);
			BarraVolume.gameObject.SetActive (false);
			CaixaModoJanela.gameObject.SetActive (false);
			Resolucoes.gameObject.SetActive (false);
			Qualidades.gameObject.SetActive (false);
			BotaoVoltar.gameObject.SetActive (true);
			BotaoSalvarPref.gameObject.SetActive (false);
		}

        Barcos.gameObject.SetActive(true);
        Barcos.onValueChanged.Invoke(0);

		Provas.gameObject.SetActive (true);
        BotaoIniciar.gameObject.SetActive(true);

	}

	public void Iniciar_Simulação()
	{
		PlayerPrefs.SetInt("barco_escolhido", Barcos.value );
		PlayerPrefs.SetInt("prova_escolhida", Provas.value) ;

		SceneManager.LoadScene (nomeCenaJogo,LoadSceneMode.Single);

		Debug.Log ("CARREGADO BARCO	"+ Barcos.value +" !");
	}

	public void Sair(){
		Application.Quit ();
	}
}