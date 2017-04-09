using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CoreState
{
	Intro,
	InProgress,
	Completed
}

public class GameCore : MonoBehaviour {

	public IGameStage CurrentStage { private set; get; }
	public DialogsCore DialogSystem { private set; get; }

	public static GameCore instance { private set; get; }

	[SerializeField]
	StageType m_curStageType;

	public CoreState stageState { get; private set; }
	
	void Awake()
	{
		if (instance != null)
			Destroy(this.gameObject);
		else
			instance = this;
		DontDestroyOnLoad(transform.gameObject);
	}	

	void LoadStage(StageType stg)
	{
		m_curStageType = stg;		
		SceneManager.LoadScene(StageTypeHelper.TypeToScene(stg));
	}

	// Use this for initialization
	void Start () {				
    }

	void InitStage()
	{
		DialogSystem.OnGameStart(()=>
		{
			stageState = CoreState.InProgress;
		});
		stageState =  CoreState.Intro;
	}
	
	// Update is called once per frame
	void Update () {

		if (stageState!= CoreState.InProgress)
			return;

		if(CurrentStage.Completed)
		{
			stageState = CoreState.Completed;
			DialogSystem.OnGameEnd(()=>			
			{
				GoNextStage();
			});
			
        }
	}

	void GoNextStage()
	{
		switch (m_curStageType)
		{
			case StageType.HandStage:
				LoadStage(StageType.HospitalStage);
				return;
			case StageType.HelpStage:
				LoadStage(StageType.HospitalStage);
				return;
		}
	}

	void OnEnable()
	{		
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		DialogSystem = FindObjectOfType<DialogsCore>();
		switch (m_curStageType)
		{
			case StageType.HandStage:
				CurrentStage = FindObjectOfType<Hand>();
				break;
			default:
			case StageType.HelpStage:
				CurrentStage = FindObjectOfType<Eye>();
				break;
		}
		InitStage();
    }

}
