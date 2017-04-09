using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : IGameStage
{
	[SerializeField]
	Text gameOverText;

	public void setDays(int days)
	{
		gameOverText.text = String.Format("Вы очнулись за {0} дней!\n\nТак держать! Это было сложно, но ты справился!\n\nСпасибо за игру!\n\n\n\n\nАвторы: Иван Суханов, Владислав Сухов", days);
	}
}

