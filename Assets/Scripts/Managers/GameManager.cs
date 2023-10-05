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
	private static PoolManager poolManager;
	private static ResourceManager resourceManager;
	private static SceneManager sceneManager;
	private static SoundManager soundManager;


	public static GameManager Instance { get { return instance; } }
	public static PoolManager Pool { get { return poolManager; } }
	public static ResourceManager Resource { get { return resourceManager; } }
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

		GameObject sceneObject = new GameObject();
		sceneObject.name = "SceneManager";
		sceneObject.transform.parent = transform;
		sceneManager = sceneObject.AddComponent<SceneManager>();

		GameObject soundObject = new GameObject();
		soundObject.name = "SoundManager";
		soundObject.transform.parent = transform;
		soundManager = soundObject.AddComponent<SoundManager>();	
	}
}