using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager; // 만든 SceneManager와 유니티 내의 SceneManager와 겹침 방지

public class SceneManager : MonoBehaviour
{
	private LoadingScene loadingScene;

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

	private void Awake()
	{
		LoadingScene loadingScene = Resources.Load<LoadingScene>("Scenes/LoadingScene");
		this.loadingScene = Instantiate(loadingScene);
		this.loadingScene.transform.SetParent(transform);
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadingRoutine(sceneName));
	}

	private IEnumerator LoadingRoutine(string sceneName)
	{
		loadingScene.SetProgress(0f);
		loadingScene.FadeOut();
		yield return new WaitForSeconds(0.5f); 
		Time.timeScale = 0f;  //로딩 중에는 게임의 시간을 멈춰줌

		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
		while (!oper.isDone)
		{
			loadingScene.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
			yield return null;

			Debug.Log("몬스터 랜덤 배치");
			loadingScene.SetProgress(0.6f);
			yield return new WaitForSecondsRealtime(1f); // 게임 시간은 멈춰있으니 실제 시간만큼 흘러가게 함 

			Debug.Log("리소스 불러오기");
			loadingScene.SetProgress(0.7f);
			yield return new WaitForSecondsRealtime(1f);

			Debug.Log("풀링");
			loadingScene.SetProgress(0.8f);
			yield return new WaitForSecondsRealtime(1f);


			Debug.Log("랜덤 아이템 배치");
			loadingScene.SetProgress(0.9f);
			yield return new WaitForSecondsRealtime(1f);

			Debug.Log("랜덤 맵 생성");
			loadingScene.SetProgress(1.0f);
			yield return new WaitForSecondsRealtime(1f);
		}

		CurScene.LoadAsync();
		if (CurScene != null)
		{
			CurScene.LoadAsync();
			while (CurScene.progress < 1f)
			{
				loadingScene.SetProgress(Mathf.Lerp(0.5f, 1f, CurScene.progress));
				yield return null;
			}
		}

		loadingScene.SetProgress(1f);
		loadingScene.FadeIn();
		Time.timeScale = 1f;
		yield return new WaitForSeconds(0.5f);
	}
}