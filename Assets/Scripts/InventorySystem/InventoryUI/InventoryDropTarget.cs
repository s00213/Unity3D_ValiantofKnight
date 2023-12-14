using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Dragging;
using Inventories;

namespace UI.Inventories
{
	/// <summary>
	/// 아이템을 게임신에 생성이 되도록 픽업을 처리함
	/// 
	/// Must be placed on the root canvas where items can be dragged. Will be
	/// called if dropped over empty space. 
	/// </summary>
	public class InventoryDropTarget : MonoBehaviour, IDragDestination<InventoryItem>
    {
        public void AddItems(InventoryItem item, int number)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ItemDropper>().DropItem(item, number);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return int.MaxValue;
        }
    }
}