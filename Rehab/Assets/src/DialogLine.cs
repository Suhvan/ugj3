using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogLine : MonoBehaviour {
	[SerializeField]
	Text text;

	[SerializeField]
	AudioSource audioSource;

	private string dialogCode;

	private Queue<string> DialogQueue;

	private System.Action onSequenceEnd;

	public void Init(string code, Queue<string> dialogQueue, System.Action onSequenceEnd = null)
	{
		dialogCode = code;
		audioSource.clip = GameCore.instance.DialogSystem.GetClip(dialogCode);        
		text.text = GameCore.instance.DialogSystem.GetText(dialogCode);
		DialogQueue = dialogQueue;
		transform.parent = Camera.main.transform;
		this.onSequenceEnd = onSequenceEnd;
    }

	// Use this for initialization
	void Start()
	{
		audioSource.Play();
		StartCoroutine(DestryIn(audioSource.clip.length));
	}

	

	private IEnumerator DestryIn(float seconds)
	{
		yield return new WaitForSeconds(seconds + 0.5f);
		Destroy(gameObject);
		if (DialogQueue != null && DialogQueue.Count > 0)
			GameCore.instance.DialogSystem.CreateDialog(DialogQueue);
		else if (onSequenceEnd != null)
			onSequenceEnd();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
