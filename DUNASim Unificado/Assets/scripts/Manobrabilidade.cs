using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manobrabilidade : MonoBehaviour {

	int minuteTimer = 0;
	float timer = 0f;
	bool running, fimProva = false;
	public Text timerText;

    void TextUI()
    {
        if (timer < 10f)
            timerText.text = minuteTimer.ToString("00") + ":0" + timer.ToString("F2");
        else
        {
            timerText.text = minuteTimer.ToString("00") + ":" + timer.ToString("F2");
        }

        if (minuteTimer > 7)
            timerText.color = new Color(255f, 0f, 0f);
    }

    // Use this for initialization
    void Start () {
		TextUI ();
	}

    private void ClearRotation()
    {
        transform.Rotate(new Vector3(-transform.rotation.eulerAngles.x,
                                      0,
                                     -transform.rotation.eulerAngles.z));
    }

	public void OnEnable()
	{
		minuteTimer = 0 ;
		timer = 0f;
        running = fimProva = false;
		Start ();
	}
	
	// Update is called once per frame
	void Update () {

        ClearRotation();

		if (running)
			timer += Time.deltaTime;
		
		if (timer >= 60) {
			timer = 0f;
			minuteTimer++;
		}

		TextUI ();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinalProva")
        {
            running = false;
            fimProva = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" & !fimProva)
            running = true;
    }
}

