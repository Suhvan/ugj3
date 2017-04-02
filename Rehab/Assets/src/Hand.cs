using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	float resist;

	[SerializeField]
	bool useScroll;

	[SerializeField]
	GameObject backGround;

	bool m_directingUp;

	bool directingUp
	{
		get
		{
			return m_directingUp;
		}
		set
		{
			m_directingUp = value;
			if (m_directingUp)
			{
				freeHand.color = Color.white;
				gripHand.color = Color.clear;
			}
			else
			{
				freeHand.color = Color.clear;
				gripHand.color = Color.white;
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
		float scrolVal = 0;
        if (useScroll)
			scrolVal = Input.GetAxis("Mouse ScrollWheel");
		else
			scrolVal = Input.GetAxis("Mouse Y");

		scrolVal /= resist;

		if (directingUp)
		{
			if (scrolVal < 0)
				return;
			transform.Translate(Vector3.up * scrolVal);	
					
            if (transform.localPosition.y >= yMax)
				directingUp = false;
			return;
		}

		if (scrolVal > 0)
			return;
		transform.Translate(Vector3.up * scrolVal);
		backGround.transform.Translate(Vector3.up * scrolVal);
		if (transform.localPosition.y <= yMin)
			directingUp = true;


	}
}
