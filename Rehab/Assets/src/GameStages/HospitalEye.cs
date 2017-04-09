using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class HospitalEye : Eye
{	
	[SerializeField]
	AudioSource music;

	[SerializeField]
	Text DaysCounter;

	[SerializeField]
	float dayDuration = 10f;

	float timeSinsePrevDay = 0;

	int daysCount = 1;


	void Start()
	{
		DaysCounter.gameObject.SetActive(false);
    }

	protected override void onUpdate()
	{
		base.onUpdate();
		if (!music.isPlaying)
			music.Play();

		if (!DaysCounter.gameObject.activeSelf)
		{
			DaysCounter.text = String.Format("{0}й ДЕНЬ", daysCount);
			DaysCounter.gameObject.SetActive(true);
		}

		timeSinsePrevDay += Time.deltaTime;
		if (timeSinsePrevDay > dayDuration)
		{
			daysCount++;
			timeSinsePrevDay = 0;
			DaysCounter.text = String.Format("{0}й ДЕНЬ", daysCount);
		}
	}
}

