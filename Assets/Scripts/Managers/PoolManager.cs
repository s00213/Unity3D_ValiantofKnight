using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
	Dictionary<string, ObjectPool<GameObject>> poolDictionary;
	Transform poolRoot;
	Canvas canvasRoot;

	private void Awake()
	{
		poolDictionary = new Dictionary<string, ObjectPool<GameObject>>();
	}

	public T Get<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : class
	{
		if (original is GameObject)
		{
			GameObject prefab = original as GameObject;

			if (!poolDictionary.ContainsKey(prefab.name))
				CreatePool(prefab.name, prefab);

			ObjectPool<GameObject> pool = poolDictionary[prefab.name];
			GameObject go = pool.Get();
			go.transform.position = position;
			go.transform.rotation = rotation;
			return go as T;
		}
		if (original is Component)
		{
			Component componenet = original as Component;
			string key = componenet.gameObject.name;

			if (!poolDictionary.ContainsKey(key))
				CreatePool(key, componenet.gameObject);

			GameObject go = poolDictionary[key].Get();
			go.transform.position = position;
			go.transform.rotation = rotation;
			return go.GetComponent<T>();
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
		if (instance is Component)
		{
			Component componenet = instance as Component;
			string key = componenet.gameObject.name;

			if (!poolDictionary.ContainsKey(key))
				return false;

			poolDictionary[key].Release(componenet.gameObject);
			return true;
		}
		else
		{
			return false;
		}
	}

	private void CreatePool(string key, GameObject prefab)
	{
		ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
		createFunc: () =>
		{
			GameObject go = Instantiate(prefab);
			go.name = key;
			return go;
		},
		actionOnGet: (GameObject go) =>
		{
			go.SetActive(true);
			go.transform.SetParent(null);
		},
		actionOnRelease: (GameObject go) =>
		{
			go.SetActive(false);
			go.transform.SetParent(transform);
		},
		actionOnDestroy: (GameObject go) =>
		{
			Destroy(go);
		}
		);
		poolDictionary.Add(key, pool);
	}
}
