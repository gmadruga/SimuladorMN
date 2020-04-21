using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barco_Prova : MonoBehaviour
{
    public GameObject barco0, barco1, barco2, barco3;
    public GameObject prova0, prova1, prova2, prova3;
    static public GameObject barco_atual, prova_atual;

    // Ativa os objetos do barco e da prova escolhidos pelo usuário
    public void Start()
	{
        Carregar_Barco();
        Carregar_Prova ();
    }

    public void Carregar_Barco()
    {

        int barco = PlayerPrefs.GetInt("barco_escolhido");

        switch (barco)
        {
            case 0:
                barco0.SetActive(true);
                barco_atual = barco0;
                break;
            case 1:
                barco1.SetActive(true);
                barco_atual = barco1;
                break;
            case 2:
                barco2.SetActive(true);
                barco_atual = barco2;
                break;
            case 3:
                barco3.SetActive(true);
                barco_atual = barco3;
                break;
        }

		barco_atual.GetComponent<Rigidbody>().velocity = barco_atual.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }


    public void Carregar_Prova()
    {
        int prova = PlayerPrefs.GetInt("prova_escolhida");

        switch (prova)
        {
            case 0:
                prova0.SetActive(true);
                prova_atual = prova0;
                break;
            case 1:
                prova1.SetActive(true);
                prova_atual = prova1;
                break;
            case 2:
                prova2.SetActive(true);
                prova_atual = prova2;
                break;
            case 3:
                prova3.SetActive(true);
                prova_atual = prova3;
                break;
        }

    }

	public void Desativar_Tudo()
	{
        barco_atual.SetActive(false);
        prova_atual.SetActive(false);
    }

}
