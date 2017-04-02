using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour {

	public DialogsCore dialogSystem;
	public static GameCore instance { private set; get; }

	// Use this for initialization
	void Start () {
		instance = this;
		dialogSystem.OnGameStart();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
