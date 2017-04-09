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

	public AudioSource Music
	{
		get
		{
			return music;
		}
	}

	public int Days
	{
		get
		{
			return daysCount;
		}
	}

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

	int tutStage = 0;


	private void ProceedTutorial()
	{
		if (tutStage == 0)
		{
			tutStage++;			
			Tutorial.instance.StartTutorial(Tutorial.TutType.SpacePress);
		}

		if (tutStage < 10 && Input.GetKeyUp(KeyCode.Space))
		{
			tutStage++;			
		}

		if (tutStage == 10)
		{
			tutStage++;
			Tutorial.instance.StopAllTutorials();
		}
	}

	protected override void onUpdate()
	{
		base.onUpdate();
		ProceedTutorial();
        if (!music.isPlaying)
			music.Play();

		if (!DaysCounter.gameObject.activeSelf)
		{
			DaysCounter.text = String.Format("ДЕНЬ {0}", daysCount);
			DaysCounter.gameObject.SetActive(true);
		}

		timeSinsePrevDay += Time.deltaTime;
		if (timeSinsePrevDay > dayDuration)
		{
			GameCore.instance.DialogSystem.PlayNext();
			daysCount++;
			timeSinsePrevDay = 0;
			DaysCounter.text = String.Format("ДЕНЬ {0}", daysCount);
		}
	}
}

