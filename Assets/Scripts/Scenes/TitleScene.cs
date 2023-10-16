using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
	public void StartButton()
	{
		GameManager.Scene.LoadScene("02_VillageScene");
	}

	public void OnApplicationQuit()
	{
		Debug.Log("Game Quit");
	}

	protected override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}
