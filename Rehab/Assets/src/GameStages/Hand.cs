using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HandState
{
	Leaning,
	WaitingGrab,
	Grabing,
	WaitingRelease
}


public class Hand : IGameStage
{

	[SerializeField]
	SpriteRenderer freeHand;

	[SerializeField]
	SpriteRenderer gripHand;

	[SerializeField]
	float yMax;

	[SerializeField]
	float yMin;

	[SerializeField]
	float PhoneTriggerDelta;

	[SerializeField]
	float resist;

	[SerializeField]
	bool useScroll;

	[SerializeField]
	GameObject backGround;

	[SerializeField]
	AnimationCurve resistCurve;

	[SerializeField]
	Transform target;

	[SerializeField]
	AudioSource pain;


	HandState m_state;
	float m_targetDistance;

	HandState State
	{
		get
		{
			return m_state;
		}
		set
		{
			m_state = value;
			switch (m_state)
			{
				case HandState.Grabing:
				case HandState.WaitingRelease:
					freeHand.color = Color.clear;
					gripHand.color = Color.white;
					break;
				case HandState.Leaning:
				case HandState.WaitingGrab:				
					freeHand.color = Color.white;
					gripHand.color = Color.clear;
					break;
			}			
		}
	}

	// Use this for initialization
	void Start () {
		freeHand.color =  Color.white;
		gripHand.color = Color.clear;
		m_targetDistance = target.position.y+2.5f;
	}

	private float distance
	{
		get
		{
			return transform.position.y - yMin - backGround.transform.position.y;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Completed)
		{
			pain.Stop();
			return;
		}

		if (GameCore.instance.stageState != CoreState.InProgress)
			return;

		if (!pain.isPlaying)
			pain.Play();

		switch (State)
		{
			case HandState.WaitingGrab:
				if (!Input.GetKeyDown(KeyCode.Space))
					return;				
				State = HandState.Grabing;
				break;
			case HandState.WaitingRelease:
				if (!Input.GetKeyUp(KeyCode.Space))
					return;				
				State = HandState.Leaning;
				break;
			case HandState.Grabing:
				if (distance >= m_targetDistance - PhoneTriggerDelta)
				{
					Completed = true;
					return;
				}
				if (!Input.GetKey(KeyCode.Space))
				{
					State = HandState.Leaning;
					return;
				}
				
				break;
			case HandState.Leaning:
				if (Input.GetKey(KeyCode.Space))
				{
					State = HandState.Grabing;
					return;
				}
				break;
		}

		float scrolVal = 0;
        if (useScroll)
			scrolVal = Input.GetAxis("Mouse ScrollWheel");
		else
			scrolVal = Input.GetAxis("Mouse Y");

		if (scrolVal == 0)
			return;		

		scrolVal /= 1 + resist  * resistCurve.Evaluate(distance / m_targetDistance);
		

		switch (State)
		{
			case HandState.Leaning:
				if (scrolVal < 0)
					return;
				transform.Translate(Vector3.up * scrolVal);

				//Debug.Log(distance + " vs " + m_targetDistance);

				if (transform.localPosition.y >= yMax || distance  >= (m_targetDistance + PhoneTriggerDelta))
					State = HandState.WaitingGrab;
				return;
			case HandState.Grabing:
				if (scrolVal > 0)
					return;
				transform.Translate(Vector3.up * scrolVal);
				backGround.transform.Translate(Vector3.up * scrolVal);
				if (transform.localPosition.y <= yMin)
					State = HandState.WaitingRelease;
				return;
		}	
	}
}
