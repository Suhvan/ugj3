using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HospitalObject : MonoBehaviour
{
	[SerializeField]
	Animator anim;

	[SerializeField]
	float startDelay = 2f;

	[SerializeField]
	float sleepTime = 1f;

	[SerializeField]
	float sleepRandomAdd = 1f;

	[SerializeField]
	int animCount = 0;

	int curState = 0;

	public float randomDelay = 0;
	public float timeSince = 0;

	void Start()
	{
		randomDelay = startDelay;
    }

	void SetRandomDelay()
	{
		randomDelay = sleepTime + Random.Range(0,sleepRandomAdd );
		timeSince = 0;
	}

	
	void Update()
	{

		if (GameCore.instance != null && GameCore.instance.stageState != CoreState.InProgress )
		{
			return;
		}

		timeSince += Time.deltaTime;

		if (timeSince > randomDelay)
		{
			StartCoroutine(playAnim());
			SetRandomDelay();
		
			
		}
    }


	IEnumerator playAnim()
	{
		var rand = Random.Range(0, animCount).ToString();
		Debug.Log("Result " + rand);
		anim.Play("Empty");
		anim.Play(rand);		
		yield return null;
		var info = anim.GetCurrentAnimatorStateInfo(0);
		randomDelay += info.length;
		Debug.Log("lngt "+ info.length);
	}





	
	

}

