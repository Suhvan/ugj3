using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogsCore : MonoBehaviour {

	[SerializeField]
	private DialogLine dlPrefab;

	[SerializeField]
	private List<string> StartDialog;

	[SerializeField]
	private StoryScreen StartScreen;

	[SerializeField]
	private List<string> EndDialog;

	[SerializeField]
	private StoryScreen EndScreen;


	// Use this for initialization
	void Awake () {
		StartScreen.Hide(null);
		EndScreen.Hide(null);
	}	
	
	// Update is called once per frame
	void Update () {
    }



	public void OnGameStart(System.Action onComplete = null)
	{
		StartScreen.Show();
		CreateDialog(new Queue<string>(StartDialog), () => {
			StartScreen.Hide(onComplete);
        });
    }

	public void OnGameEnd(System.Action onComplete)
	{
		EndScreen.Show();
		CreateDialog(new Queue<string>(EndDialog), onComplete);
	}

	public void CreateDialog(string code)
	{
		var dialog = Instantiate(dlPrefab);
		dialog.Init(code, null);
	}

	public void CreateDialog(Queue<string> dialogQueue, System.Action onComplete = null )
	{
		var dialog = Instantiate(dlPrefab);
		dialog.Init(dialogQueue.Dequeue(), dialogQueue, onComplete);
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
		{			
			Debug.LogError("Failed to load Text/" + clipName);
			return string.Empty;
        }
		return txt.text;
	}

}
