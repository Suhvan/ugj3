
public enum StageType
{
	HandStage,
	HelpStage,
	HospitalStage,
	GameOver
}

public static class StageTypeHelper
{

	public static string TypeToScene(StageType st)
	{
		switch (st)
		{
			case StageType.HandStage:
				return "hands";
			case StageType.GameOver:
				return "gameOver";
			default:
			case StageType.HelpStage:
				return "first";
			case StageType.HospitalStage:
				return "hospital";
		}
	}

	public static StageType SceneToType(string sceneName)
	{
		switch (sceneName)
		{
			case "hands":
				return StageType.HandStage;
			default:
			case "first":
				return StageType.HelpStage;
			case "hospital":
				return StageType.HospitalStage;
			case "gameOver":
				return StageType.GameOver;
        }
	}
}

