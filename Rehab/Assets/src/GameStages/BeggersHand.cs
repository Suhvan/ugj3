using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeggersHand : EyeDependent {

	[SerializeField]
	Transform startPos;

	[SerializeField]
	Transform endPos;

	[SerializeField]
	Transform hand;

	[SerializeField]
	float endScale;

	float distance;

	float scaleDelta;

	public override void ApplyStep(float step)
	{
		base.ApplyStep(step);
		var target = startPos;
        if (step < 0)
		{
			step = -step;
			target = endPos;
        }

		hand.position = Vector3.MoveTowards(hand.position, target.position, step);

		hand.localScale = Vector3.one * ScaleCoef;
	}


	private float ScaleCoef
	{
		get
		{
			return 1 + scaleDelta * Mathf.Abs((hand.position.y - startPos.position.y)/ distance);
		}
	}

	// Use this for initialization
	void Start () {
		hand.position = startPos.position;
		distance = endPos.position.y - startPos.position.y;
		scaleDelta = endScale - 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
