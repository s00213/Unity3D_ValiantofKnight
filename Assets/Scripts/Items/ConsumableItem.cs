using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
	public ConsumableData consumableData;

	//public string itemType;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("¾ÆÀÌÅÛ : " + consumableData.itemName);
			if(consumableData.itemName.Contains("HPPortion 100"))
			{
				//print(consumableData.itemName + " " + consumableData.amount);
			if(DataManager.instance.HPpotionCount>0)
			{
					DataManager.instance.HPpotionCount += consumableData.amount;
			}
			else{
					Inventory.instance.AddItem(consumableData);
					DataManager.instance.HPpotionCount += consumableData.amount;
				}

			}
			
		}
		Destroy(gameObject);
	}

	public void SelfDestory()
	{ 
		Destroy(gameObject);
	}
}
