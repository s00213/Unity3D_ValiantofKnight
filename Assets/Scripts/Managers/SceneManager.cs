using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scene { LOBBY, LOADING, GAME, TEMP, SCORE };

public class SceneManager : MonoBehaviour
{
	private Scene currentScene;
	public Scene CurrentScene { get { return currentScene; } }

	public void LoadScene(Scene scene)
	{
		currentScene = scene;
		StartCoroutine(LoadingRoutine(scene));
	}

	IEnumerator LoadingRoutine(Scene scene)
	{
		GameManager.Sound.Clear();
		yield return new WaitUntil(() => { return GameManager.Sound.IsMuted(); });

		GameManager.Sound.FadeInAudio();
		yield return new WaitWhile(() => { return GameManager.Sound.IsMuted(); });

		yield break;
	}
}