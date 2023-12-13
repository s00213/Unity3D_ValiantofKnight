using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Dragging;
using Inventories;

namespace UI.Inventories
{
	/// <summary>
	/// 아이템이 세상에 떨어졌을 때 생성되는 픽업을 처리하며,
	/// 빈 공간 위에 놓으면 호출됨
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