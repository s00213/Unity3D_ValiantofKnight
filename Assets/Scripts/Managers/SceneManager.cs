using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;



public class SceneManager : MonoBehaviour
{
	private BaseScene curScene;
	public BaseScene CurScene
	{
		get
		{
			if (curScene == null)
				curScene = GameObject.FindObjectOfType<BaseScene>();

			return curScene;
		}
	}

	internal static AsyncOperation LoadSceneA(string sceneName)
	{
		throw new NotImplementedException();
	}

	// ToDo : UI Manager로 바인딩하는 것이 더 좋음
	public void LoadScene(string sceneName)
	{
		UnitySceneManager.LoadSceneAsync(sceneName);
	}
}