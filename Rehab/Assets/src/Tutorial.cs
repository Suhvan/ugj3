using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public enum TutType
	{
		MouseUp,
		MouseDown,
		SpaceHold
	}

	[SerializeField]
	GameObject holdSpace;

	[SerializeField]
	GameObject mouseUp;

	[SerializeField]
	GameObject mouseDown;

	public static Tutorial instance { private set; get; }

	void Awake()
	{
		if (instance != null)
			Destroy(this.gameObject);
		else
			instance = this;
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartTutorial(TutType tt)
	{
		SetActive(tt, true);
	}

	public void StopTutorial(TutType tt)
	{
		SetActive(tt, false);
	}

	private void SetActive(TutType tt, bool active)
	{
		switch (tt)
		{
			case TutType.MouseDown:
				mouseDown.SetActive(active);
                break;
			case TutType.MouseUp:
				mouseUp.SetActive(active);
				break;
			case TutType.SpaceHold:
				holdSpace.SetActive(active);
				break;
		}
	}
}
