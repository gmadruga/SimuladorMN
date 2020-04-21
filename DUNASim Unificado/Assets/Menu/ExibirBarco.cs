using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExibirBarco : MonoBehaviour

    // Controla a exibição 3D dos barcos no menu de seleção 
{
	public GameObject barco1, barco2, moana, legado;
    public GameObject centro2; // para ajustar a posição de legado e moana

	//public Dropdown selecao;
	static GameObject escolhido;
	public int valor_inicial;

    readonly Vector3 vista = new Vector3(1.5f, 3.2f, 2.2f);

    public void Show ( Dropdown selecao)
    {
        end();

        switch (selecao.value) 
		{
		case 0:
			valor_inicial = 0;
			escolhido = barco1;
			break;
		case 1:
			valor_inicial = 1;
			escolhido = barco2;
			break;
		case 2:
			valor_inicial = 2;
			escolhido = moana;
			break;
		case 3:
			valor_inicial = 3;
			escolhido = legado;
			break;
		}
		Debug.Log ("detectado" + escolhido.ToString());

		escolhido = Instantiate (escolhido, vista , Quaternion.identity);

        Rigidbody rb = escolhido.GetComponent<Rigidbody>();

        rb.centerOfMass = centro2.transform.position ;

        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.mass = 1;
        rb.angularDrag = 0;
        rb.angularVelocity = Vector3.up;
		}

	public void end()
	{
		Destroy( escolhido );
	}
}

	