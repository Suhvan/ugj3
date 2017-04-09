using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : IGameStage
{

	[SerializeField]
	GameObject topVeko;

	[SerializeField]
	GameObject botVeko;

	[SerializeField]
	float closingSpeed;

	[SerializeField]
	float openingImpulse;

	[SerializeField]
	float openingDuration;

	[SerializeField]
	AnimationCurve declineCurve;

	[SerializeField]
	AnimationCurve resistCurve;

	[SerializeField]
	float yStopValue;

	[SerializeField]
	EyeDependent eyeDependent;

	float timeSinceOpen = int.MaxValue;


	float openinigSpeed
	{
		get
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				timeSinceOpen = 0;
			}

			if (timeSinceOpen > openingDuration)
			{
				return 0;
			}

			timeSinceOpen += Time.deltaTime;
			return declineCurve.Evaluate(timeSinceOpen / openingDuration) * openingImpulse;
		}
	}

	float resist
	{
		get
		{
			return resistCurve.Evaluate(topVeko.transform.localPosition.y / yStopValue);
        }
	}

	

	// Use this for initialization
	void Start()
	{
		Completed = false;
	}

	protected virtual void onUpdate()
	{

	}

	void Update () {

		if (Completed)
			return;

		if (GameCore.instance.stageState != CoreState.InProgress)
			return;

		onUpdate();
		var speed = closingSpeed * resist;
		speed-= openinigSpeed;

		float step = speed * Time.deltaTime;

		if (eyeDependent != null)
			eyeDependent.ApplyStep(step);

		if (step > 0)
		{
			topVeko.transform.localPosition = Vector3.MoveTowards(topVeko.transform.localPosition, Vector3.zero, step);
			botVeko.transform.localPosition = Vector3.MoveTowards(botVeko.transform.localPosition, Vector3.zero, step);
		}
		else
		{
			step = -step;
			topVeko.transform.localPosition = Vector3.MoveTowards(topVeko.transform.localPosition, Vector3.up * yStopValue, step);
			botVeko.transform.localPosition = Vector3.MoveTowards(botVeko.transform.localPosition, Vector3.down  * yStopValue, step);
			if (topVeko.transform.localPosition.y >= yStopValue)
				Completed = true;
		}

	}
}
