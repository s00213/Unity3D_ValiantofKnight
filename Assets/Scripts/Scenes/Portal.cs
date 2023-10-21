using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Portal : MonoBehaviour
{
	[SerializeField] private string sceneToLoad;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") || other.CompareTag("Pet"))
		{
			GameManager.Scene.LoadScene(sceneToLoad);
			Debug.Log("Enters the portal");
		}
	}
}
