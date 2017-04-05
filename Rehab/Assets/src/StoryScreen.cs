using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScreen : MonoBehaviour {

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide(System.Action onHide)
	{
		gameObject.SetActive(false);
		if (onHide != null)
			onHide();
	}

	// Use this for initialization
	void Awake () {
		Hide(null);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
