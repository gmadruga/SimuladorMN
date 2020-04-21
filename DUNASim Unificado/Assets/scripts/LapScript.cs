using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapScript : MonoBehaviour {

    public bool AllFloaterRed = false;
	byte lap = 0;
	bool start = false;
	bool firstTry = true;
	int minuteBestTime = 0;
	float bestTime = 0f;
	int minuteTimer = 0;
	float timer = 0f;
	int minuteFirstLap = 0;
	float firstLapTimer = 0f;
	int minuteSecondLap = 0;
	float secondLapTimer = 0f;
	//float thirdLapTimer = 0f;
	public Text lapText;
	public Text timerText;
	public Text firstLapText;
	public Text secondLapText;
	public Text thirdLapText;
	public Text bestTimeText;
	public PassouBoia[] marcadores;

	// Use this for initialization
	void Start () {
		TextUI ();
	}

	void Update () {
		if (start) {
			timer += Time.deltaTime;
			if (timer >= 60) {
				timer = 0f;
				minuteTimer++;
			}

			TextUI ();
		}
	}

	private bool LegalLap () // verificar se boias estão todas verdes
	{
		
		for (int i = 0; i < marcadores.Length ; i++){ 

			if (!marcadores [i].ultrapassada)
				return false; // ilegal lap
		}

		return true; // legal lap
	}

	private void OnTriggerEnter (Collider other) {

        AllFloaterRed = true;
        //if (other.CompareTag ("Player")) {
			if (lap == 0) {
				lap++;
				start = true;
			} else {
			if (lap == 1 && LegalLap() ) {
					firstLapTimer = timer;
					minuteFirstLap = minuteTimer;
					lap++;
				} else {
					if (lap == 2 && LegalLap()) {
						if (timer < firstLapTimer) {
							minuteSecondLap = minuteTimer - minuteFirstLap - 1;
							secondLapTimer = 60f + timer - firstLapTimer;
						} else {
							secondLapTimer = timer - firstLapTimer;
							minuteSecondLap = minuteTimer - minuteFirstLap;
						}
						lap++;
					} else {
						if (lap == 3 && LegalLap()) {
							//thirdLapTimer = timer - firstLapTimer - secondLapTimer;

							if (firstTry) {
								bestTime = timer;
								minuteBestTime = minuteTimer;
							}
							
							else {
								if (minuteTimer <= minuteBestTime) {
									if (timer < bestTime) {
										bestTime = timer;
										minuteBestTime = minuteTimer;
									}
								}
							}

							lap = 1;
							firstLapTimer = 0f;
							secondLapTimer = 0f;
							//thirdLapTimer = 0f;
							timer = 0f;
							minuteTimer = 0;
							firstTry = false;
						}
					}
				}
			}
		//}
	}

	void TextUI ()
	{
		lapText.text = "Lap: " + lap.ToString () + "/3";

		if (timer < 10f)
			timerText.text = minuteTimer.ToString("00") + ":0" + timer.ToString ("F2");
		else {
			timerText.text = minuteTimer.ToString("00") + ":" + timer.ToString ("F2");
		}

		if (firstLapTimer < 10f)
			firstLapText.text = "1st Lap: " + minuteFirstLap.ToString("00") + ":0"+ firstLapTimer.ToString ("F2");
		else {
			firstLapText.text = "1st Lap: " + minuteFirstLap.ToString("00") + ":"+ firstLapTimer.ToString ("F2");
		}

		if (secondLapTimer < 10f)
			secondLapText.text = "2nd Lap: " + minuteSecondLap.ToString("00") + ":0" + secondLapTimer.ToString ("F2");
		else {
			secondLapText.text = "2nd Lap: " + minuteSecondLap.ToString("00") + ":" + secondLapTimer.ToString ("F2");
		}
			
		if (bestTime < 10f)
			bestTimeText.text = "Best Time: " + minuteBestTime.ToString("00") + ":0" + bestTime.ToString ("F2");
		else {
			bestTimeText.text = "Best Time: " + minuteBestTime.ToString("00") + ":" + bestTime.ToString ("F2");
		}

		if (minuteTimer > 4)
			timerText.color = new Color (255f, 0f, 0f);

		//thirdLapText.text = "3rd Lap: " + thirdLapTimer.ToString ("F3");
	}

    public void OnEnable()
    {
		minuteBestTime = 0;
		bestTime = 0f;
		minuteTimer = 0;
		timer = 0f;
		minuteFirstLap = 0;
		firstLapTimer = 0f;
		minuteSecondLap = 0;
		secondLapTimer = 0f;
		lap = 0;
         AllFloaterRed = start = false;
         firstTry = true;
		Start();
    }
}
