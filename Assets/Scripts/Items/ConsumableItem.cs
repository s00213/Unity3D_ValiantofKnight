using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
	public ConsumableData consumableData;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("æ∆¿Ã≈€ : " + consumableData.itemName);
			Inventory.instance.AddItem(consumableData);
		}
		Destroy(gameObject);
	}

	public void SelfDestory()
	{ 
		Destroy(gameObject);
	}
}
