using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour {

	public Eye Eye;
	public DialogsCore dialogSystem;
	public static GameCore instance { private set; get; }

	bool stageCompleted;

	// Use this for initialization
	void Start () {
		instance = this;
		dialogSystem.OnGameStart();
		stageCompleted = false;
    }
	
	// Update is called once per frame
	void Update () {

		if (stageCompleted)
			return;

		if(Eye.Completed)
		{
			stageCompleted = true;
			dialogSystem.OnGameEnd();
		}
	}
	
}
