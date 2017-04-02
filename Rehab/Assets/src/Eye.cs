﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

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
	float yStopValue;

	float timeSinceOpen = int.MaxValue;

	public bool Completed { get; private set; }

	// Use this for initialization
	void Start () {
		Completed = false;
    }

	float GetOpenSpeed()
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
	
	void Update () {

		if (Completed)
			return;

		var speed = closingSpeed;
		speed-=GetOpenSpeed();

		float step = speed * Time.deltaTime;
		if (step > 0)
		{
			topVeko.transform.localPosition = Vector3.MoveTowards(topVeko.transform.localPosition, Vector3.zero, step);
			botVeko.transform.localPosition = Vector3.MoveTowards(botVeko.transform.localPosition, Vector3.zero, step);
		}
		else
		{
			step = -step;
			topVeko.transform.localPosition = Vector3.MoveTowards(topVeko.transform.localPosition, Vector3.up * 10, step);
			botVeko.transform.localPosition = Vector3.MoveTowards(botVeko.transform.localPosition, Vector3.down  * 10, step);
			if (topVeko.transform.localPosition.y > yStopValue)
				Completed = true;
		}

	}
}
