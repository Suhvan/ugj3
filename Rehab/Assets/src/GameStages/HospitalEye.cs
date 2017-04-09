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

	[SerializeField]
	AnimationCurve resistDecayCurve;

	[SerializeField]
	int daysToResistDecay = 1;

	float timeSinsePrevDay = 0;

	int daysCount = 1;


	private float partOfDay
	{
		get
		{
			if (dayDuration == 0)
				return 0;
			return timeSinsePrevDay / dayDuration;
        }
		
	}
	public float resistCoef;
	public float evalVal;

	void Start()
	{
		DaysCounter.gameObject.SetActive(false);
    }

	protected override float resist
	{
		get
		{
			evalVal = (float)((daysCount - 1) + partOfDay) / (float)daysToResistDecay;
            resistCoef = resistDecayCurve.Evaluate(evalVal);
            return base.resist * resistCoef;
		}
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
			GameCore.instance.DialogSystem.PlayNext();
			daysCount++;
			timeSinsePrevDay = 0;
			DaysCounter.text = String.Format("{0}й ДЕНЬ", daysCount);
		}
	}
}

