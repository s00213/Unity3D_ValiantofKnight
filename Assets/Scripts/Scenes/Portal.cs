using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class Portal : MonoBehaviour
{
	[SerializeField] private string sceneToLoad;


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") || other.CompareTag("Pet"))
		{
			UnitySceneManager.LoadSceneAsync(sceneToLoad);
			Debug.Log("Enter the portal + " + other.tag);
		}
	}
}
