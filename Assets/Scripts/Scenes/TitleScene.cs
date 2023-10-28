using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{

	private void Awake()
	{
		Debug.Log("TitleScene Init");
	}

	protected override IEnumerator LoadingRoutine()
	{
		Debug.Log("TitleScene Load");
		progress = 0.0f;
		yield return null;
		progress = 1.0f;
		Debug.Log("TitleScene Loaded");
	}

	public void StartButton()
	{
		GameManager.Scene.LoadScene("VillageScene");
	}

	public void OnApplicationQuit()
	{
		Debug.Log("Game Quit");
	}	
}
