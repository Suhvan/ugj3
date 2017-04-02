using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogsCore : MonoBehaviour {

	[SerializeField]
	private DialogLine dlPrefab;

	[SerializeField]
	private List<string> StartDialog;

	[SerializeField]
	private List<string> EndDialog;


	// Use this for initialization
	void Start () {
		
    }
	
	// Update is called once per frame
	void Update () {
    }

	

	public void OnGameStart()
	{
		CreateDialog(new Queue<string>(StartDialog));
    }

	public void OnGameEnd()
	{
		CreateDialog(new Queue<string>(EndDialog));
	}

	public void CreateDialog(string code)
	{
		var dialog = Instantiate(dlPrefab);
		dialog.Init(code, null);
	}

	public void CreateDialog(Queue<string> dialogQueue)
	{
		var dialog = Instantiate(dlPrefab);
		dialog.Init(dialogQueue.Dequeue(), dialogQueue);
	}

	public AudioClip GetClip(string clipName)
	{
		var clip = Resources.Load<AudioClip>("Voice/" + clipName);
		if (clip == null)
			Debug.LogError("Failed to load Voice/" + clipName);
		return clip;
	}

	public string GetText(string clipName)
	{
		var txt = Resources.Load<TextAsset>("Text/" + clipName);
		if (txt == null)
			Debug.LogError("Failed to load Text/" + clipName);
		return txt.text;
	}

}
