using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProvaForca : MonoBehaviour {

	public bool provaRodando = false, Fimprova = false;
	float timer = 0f;
    int minuteTimer = 0;
    public float massScale;
    public FixedJoint fixacao;

	public Text timerText;

	// Use this for initialization
	void Start () {

        TextUI ();
        fixacao = gameObject.AddComponent<FixedJoint>();
        Reconfigurar_Fixacao();
    }


    private void Reconfigurar_Fixacao()
    {
        //WaitForSeconds ;
        GameObject barco_atual = Barco_Prova.barco_atual;
        fixacao.connectedBody = barco_atual.GetComponent<Rigidbody>();
        fixacao.massScale = massScale;
    }

    // Update is called once per frame
    void Update () {

        if (Input.anyKeyDown)
            provaRodando = true;

        if (fixacao.connectedBody == null)
        {
            Rigidbody barco_rb = Barco_Prova.barco_atual.GetComponent<Rigidbody>();
            barco_rb.position = Vector3.zero;
            barco_rb.rotation = Quaternion.identity;
            barco_rb.velocity = Vector3.zero;
            Reconfigurar_Fixacao();
        }

		if (provaRodando && !Fimprova)
        {
			timer += Time.deltaTime;
			if (timer >= 60)
            {
				timer = 0f;
				minuteTimer++;
			}
		}
		TextUI ();
	}

	private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Finish"))
        {
            provaRodando = !provaRodando;
            Fimprova = true;
        }
	}

	private void TextUI() {
		if (timer < 10f)
			timerText.text = minuteTimer.ToString ("00") + ":0" + timer.ToString ("F2");
		else {
			timerText.text = minuteTimer.ToString ("00") + ":" + timer.ToString ("F2");
		}

		if (minuteTimer > 7)
			timerText.color = new Color (255f, 0f, 0f);
	}

    public void OnEnable()
    {
        minuteTimer = 0;
        timer = 0f;
        provaRodando = Fimprova = false;
		Start ();
    }
}
