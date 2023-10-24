using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	private static ResourceManager resourceManager;
	private static PoolManager poolManager;
	private static UIManager uiManager;
	private static SceneManager sceneManager;
	private static SoundManager soundManager;

	public static GameManager Instance { get { return instance; } }
	public static ResourceManager Resource { get { return resourceManager; } }
	public static PoolManager Pool { get { return poolManager; } }
	public static UIManager UI { get { return uiManager; } }
	public static SceneManager Scene { get { return sceneManager; } }
	public static SoundManager Sound { get { return soundManager; } }

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);

		InitManagers();

		//GameManager.Sound.PlayBgm(true);
		// SoundManager의 eunm 가지고 옴
		// GameManager.Sound.PlaySfx(SoundManager.Sfx.Select);
	}

	private void OnDestroy()
	{
		if (instance == this)
			instance = null;
	}

	private void InitManagers()
	{
		GameObject resourceObject = new GameObject();
		resourceObject.name = "ResourceManager";
		resourceObject.transform.parent = transform;
		resourceManager = resourceObject.AddComponent<ResourceManager>();

		GameObject poolObject = new GameObject();
		poolObject.name = "PoolManager";
		poolObject.transform.parent = transform;
		poolManager = poolObject.AddComponent<PoolManager>();

		GameObject uiObject = new GameObject();
		uiObject.name = "UIManager";
		uiObject.transform.parent = transform;
		uiManager = uiObject.AddComponent<UIManager>();

		GameObject sceneObject = new GameObject();
		sceneObject.name = "SceneManager";
		sceneObject.transform.parent = transform;
		sceneManager = sceneObject.AddComponent<SceneManager>();

		GameObject soundObject = new GameObject();
		soundObject.name = "SoundManager";
		soundObject.transform.parent = transform;
		soundManager = soundObject.AddComponent<SoundManager>();
	}

	// 게임의 종료 여부를 저장할 멤버 변수
	private bool isGameOver;
	// 게임의 종료 여부를 저장할 프로퍼티
	public bool IsGameOver
	{
		get { return isGameOver; }
		set
		{
			isGameOver = value;
			if (isGameOver)
			{
				CancelInvoke("CreateMonster");
				//GameManager.Sound.PlaySfx(SoundManager.Sfx.Lose);
				//GameManager.Sound.PlayBgm(false);
			}
		}
	}
}
