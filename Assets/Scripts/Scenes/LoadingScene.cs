using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
	public static string nextScene;
	private float progress;

	private void Awake()
	{
		progress = 0.0f;
	}

	private void Start()
	{
		StartCoroutine(LoadingRoutine());
	}

	public static void LoadScene(string sceneName)
	{
		nextScene = sceneName;
		//SceneManager.LoadScene("LoadingScene");
	}

	IEnumerator LoadingRoutine()
	{
		while (progress < 1f)
		{
			progress += Time.deltaTime * 0.2f;

			yield return null;
		}

		//GameManager.Scene.LoadScene();
		yield return new WaitForSecondsRealtime(0.1f);
	}
}
