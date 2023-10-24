using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
	Dictionary<string, ObjectPool<GameObject>> poolDictionary;
	Dictionary<string, Transform> poolContainer;
	private Transform poolRoot;
	private Canvas canvasRoot;

	private void Awake()
	{
		poolDictionary = new Dictionary<string, ObjectPool<GameObject>>();
		poolContainer = new Dictionary<string, Transform>();
		poolRoot = new GameObject("PoolRoot").transform;
		// UI 풀링
		canvasRoot = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
	}

	public void PoolRestart()
	{
		Awake();
	}

	public T Get<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
	{
		if (original is GameObject)
		{
			GameObject prefab = original as GameObject;
			string key = prefab.name;

			if (!poolDictionary.ContainsKey(key))
				CreatePool(key, prefab);

			GameObject obj = poolDictionary[key].Get();
			obj.transform.parent = parent;
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			return obj as T;
		}
		else if (original is Component)
		{
			Component component = original as Component;
			string key = component.gameObject.name;

			if (!poolDictionary.ContainsKey(key))
				CreatePool(key, component.gameObject);

			GameObject obj = poolDictionary[key].Get();
			obj.transform.parent = parent;
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			return obj.GetComponent<T>();
		}
		else
		{
			return null;
		}
	}

	public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
	{
		return Get<T>(original, position, rotation, null);
	}

	public T Get<T>(T original, Transform parent) where T : Object
	{
		return Get<T>(original, Vector3.zero, Quaternion.identity, parent);
	}

	public T Get<T>(T original) where T : Object
	{
		return Get<T>(original, Vector3.zero, Quaternion.identity, null);
	}

	public bool Release<T>(T instance) where T : Object
	{
		if (instance is GameObject)
		{
			GameObject go = instance as GameObject;
			string key = go.name;

			if (!poolDictionary.ContainsKey(key))
				return false;

			poolDictionary[key].Release(go);
			return true;
		}
		else if (instance is Component)
		{
			Component component = instance as Component;
			string key = component.gameObject.name;

			if (!poolDictionary.ContainsKey(key))
				return false;

			poolDictionary[key].Release(component.gameObject);
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool IsContain<T>(T original) where T : Object
	{
		if (original is GameObject)
		{
			GameObject prefab = original as GameObject;
			string key = prefab.name;

			if (poolDictionary.ContainsKey(key))
				return true;
			else
				return false;

		}
		else if (original is Component)
		{
			Component component = original as Component;
			string key = component.gameObject.name;

			if (poolDictionary.ContainsKey(key))
				return true;
			else
				return false;
		}
		else
		{
			return false;
		}
	}

	private void CreatePool(string key, GameObject prefab)
	{
		GameObject root = new GameObject();
		root.gameObject.name = $"{key}Container";
		root.transform.parent = poolRoot;
		poolContainer.Add(key, root.transform);

		ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
			createFunc: () =>
			{
				GameObject obj = Instantiate(prefab);
				obj.gameObject.name = key;
				return obj;
			},
			actionOnGet: (GameObject obj) =>
			{
				obj.gameObject.SetActive(true);
				obj.transform.parent = null;
			},
			actionOnRelease: (GameObject obj) =>
			{
				obj.gameObject.SetActive(false);
				obj.transform.parent = poolContainer[key];
			},
			actionOnDestroy: (GameObject obj) =>
			{
				Destroy(obj);
			}
			);
		poolDictionary.Add(key, pool);
	}

	public T GetUI<T>(T original, Vector3 position) where T : Object
	{
		if (original is GameObject)
		{
			GameObject prefab = original as GameObject;
			string key = prefab.name;

			if (!poolDictionary.ContainsKey(key))
				CreateUIPool(key, prefab);

			GameObject obj = poolDictionary[key].Get();
			obj.transform.position = position;
			return obj as T;
		}
		else if (original is Component)
		{
			Component component = original as Component;
			string key = component.gameObject.name;

			if (!poolDictionary.ContainsKey(key))
				CreateUIPool(key, component.gameObject);

			GameObject obj = poolDictionary[key].Get();
			obj.transform.position = position;
			return obj.GetComponent<T>();
		}
		else
		{
			return null;
		}
	}

	public T GetUI<T>(T original) where T : Object
	{
		if (original is GameObject)
		{
			GameObject prefab = original as GameObject;
			string key = prefab.name;

			if (!poolDictionary.ContainsKey(key))
				CreateUIPool(key, prefab);

			GameObject obj = poolDictionary[key].Get();
			return obj as T;
		}
		else if (original is Component)
		{
			Component component = original as Component;
			string key = component.gameObject.name;

			if (!poolDictionary.ContainsKey(key))
				CreateUIPool(key, component.gameObject);

			GameObject obj = poolDictionary[key].Get();
			return obj.GetComponent<T>();
		}
		else
		{
			return null;
		}
	}

	public bool ReleaseUI<T>(T instance) where T : Object
	{
		if (instance is GameObject)
		{
			GameObject go = instance as GameObject;
			string key = go.name;

			if (!poolDictionary.ContainsKey(key))
				return false;

			poolDictionary[key].Release(go);
			return true;
		}
		else if (instance is Component)
		{
			Component component = instance as Component;
			string key = component.gameObject.name;

			if (!poolDictionary.ContainsKey(key))
				return false;

			poolDictionary[key].Release(component.gameObject);
			return true;
		}
		else
		{
			return false;
		}
	}

	private void CreateUIPool(string key, GameObject prefab)
	{
		ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
			createFunc: () =>
			{
				GameObject obj = Instantiate(prefab);
				obj.gameObject.name = key;
				return obj;
			},
			actionOnGet: (GameObject obj) =>
			{
				obj.gameObject.SetActive(true);
			},
			actionOnRelease: (GameObject obj) =>
			{
				obj.gameObject.SetActive(false);
				// 보관하는 위치를 canvasRoot로 설정
				obj.transform.SetParent(canvasRoot.transform, false);
			},
			actionOnDestroy: (GameObject obj) =>
			{
				Destroy(obj);
			}
			);
		poolDictionary.Add(key, pool);
	}
}