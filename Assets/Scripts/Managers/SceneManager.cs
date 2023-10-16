using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager; // 현재 SceneManager와 유니티 내의 SceneManager와 겹침 방지

// 각 씬 마다 있는 BaseScene 찾아서 가져다 줌
public class SceneManager : MonoBehaviour
{
	private LoadingUI loadingUI;

	private BaseScene curScene;
	public BaseScene CurScene
	{
		get
		{
			// FindObjectOfType을 자주 쓰면 부담이 되니까 Null로 확인함
			if (curScene == null)
				curScene = GameObject.FindObjectOfType<BaseScene>(); 

			return curScene;
		}
	}

	private void Awake()
	{
		LoadingUI loadingUI = GameManager.Resource.Load<LoadingUI>("UI/LoadingUI");
		loadingUI = Instantiate(loadingUI);
		loadingUI.transform.SetParent(transform, false);
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadingRoutine(sceneName));
	}

	private IEnumerator LoadingRoutine(string sceneName)
	{
		loadingUI.SetProgress(0f);
		loadingUI.FadeOut();
		yield return new WaitForSeconds(0.5f);
		//로딩 중에는 게임의 시간을 멈춰줌
		Time.timeScale = 0f;  

		// 비동기식 로딩
		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
		
		while (!oper.isDone)
		{
			loadingUI.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
			yield return null;		
		}

		// 추가적인 씬에서 준비할 로딩을 진행하고 넘어가야 함
		// 임의 페이크 로딩
		Debug.Log("몬스터 랜덤 배치");
		loadingUI.SetProgress(0.6f);
		//// 게임 시간은 멈춰있으니 실제 시간만큼 흘러가게 함
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("리소스 불러오기");
		loadingUI.SetProgress(0.7f);
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("풀링");
		loadingUI.SetProgress(0.8f);
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("랜덤 아이템 배치");
		loadingUI.SetProgress(0.9f);
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("랜덤 맵 생성");
		loadingUI.SetProgress(1.0f);
		yield return new WaitForSecondsRealtime(1f);

		//로딩 중에는 게임의 시간을 멈춘 것 해제
		Time.timeScale = 1f;
		loadingUI.FadeIn();
		yield return new WaitForSeconds(1f);
	}
}