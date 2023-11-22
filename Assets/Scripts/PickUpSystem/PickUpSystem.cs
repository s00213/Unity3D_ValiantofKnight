using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
	[SerializeField]private InventoryData inventoryData;

	private void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			Item item = other.GetComponent<Item>();
			if (item != null)
			{
				int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
				if (reminder == 0)
					item.DestroyItem();
				else
					item.Quantity = reminder;
			}
		}
	}
}
