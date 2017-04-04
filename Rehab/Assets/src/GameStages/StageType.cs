
public enum StageType
{
	HandStage,
	HelpStage,
	HospitalStage
}

public static class StageTypeHelper
{

	public static string TypeToScene(StageType st)
	{
		switch (st)
		{
			case StageType.HandStage:
				return "hands";
			default:
			case StageType.HelpStage:
				return "first";
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
		}
	}
}

