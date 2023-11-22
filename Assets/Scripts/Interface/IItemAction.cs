using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 체력 회복
public interface IItemAction
{
	public string ActionName { get; }

	public AudioClip actionSFX { get; }
	
	bool PerformAction(GameObject character, List<ItemParameter> itemState);
}
