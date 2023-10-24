using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Portal : MonoBehaviour
{
	[SerializeField] private string sceneToLoad;


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") || other.CompareTag("Pet"))
		{
			GameManager.Scene.LoadScene(sceneToLoad);
			Debug.Log("Enter the portal + " + other.tag);
		}
	}
}
