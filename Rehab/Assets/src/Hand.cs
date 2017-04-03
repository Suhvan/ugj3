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


public class Hand : MonoBehaviour {

	[SerializeField]
	SpriteRenderer freeHand;

	[SerializeField]
	SpriteRenderer gripHand;

	[SerializeField]
	float yMax;

	[SerializeField]
	float yMin;

	[SerializeField]
	float MaxDistance;	

	[SerializeField]
	float resist;

	[SerializeField]
	bool useScroll;

	[SerializeField]
	GameObject backGround;

	[SerializeField]
	AnimationCurve resistCurve;

	HandState m_state;

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
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
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
		var distance = transform.position.y - yMin - backGround.transform.position.y;
        scrolVal /= 1 + resist  * resistCurve.Evaluate(distance / MaxDistance);

		Debug.Log(string.Format("Cur resistL {0}", 1 + resist * resistCurve.Evaluate(distance / MaxDistance)));

		switch (State)
		{
			case HandState.Leaning:
				if (scrolVal < 0)
					return;
				transform.Translate(Vector3.up * scrolVal);

				if (transform.localPosition.y >= yMax)
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
