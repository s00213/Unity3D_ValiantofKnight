using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
	public void StartButton()
	{
		GameManager.Scene.LoadScene("VillageScene");
	}

	public void OnApplicationQuit()
	{
		Debug.Log("Game Quit");
	}
}
