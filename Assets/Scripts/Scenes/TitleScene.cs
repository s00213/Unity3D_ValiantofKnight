using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
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
