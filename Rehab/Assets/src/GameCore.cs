using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour {
		
	public IGameStage CurrentStage;

	public DialogsCore DialogSystem;
	public static GameCore instance { private set; get; }

	[SerializeField]
	StageType m_curStageType;

	bool stageCompleted;
	
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
		DialogSystem.OnGameStart();
		stageCompleted = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (stageCompleted)
			return;

		if(CurrentStage.Completed)
		{
			stageCompleted = true;
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
				LoadStage(StageType.HelpStage);
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
