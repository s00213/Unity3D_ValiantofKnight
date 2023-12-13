using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventories;

namespace UI.Inventories
{
	/// <summary>
	/// 올바른 정보를 표시하도록 함
	/// </summary>
	public interface IItemHolder
    {
        InventoryItem GetItem();
    }
}