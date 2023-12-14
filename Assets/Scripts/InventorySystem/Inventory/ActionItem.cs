using System;
using UnityEngine;

namespace Inventories
{
	/// <summary>
	/// 퀵 슬롯에 배치할 수 있고 소모할 수 있는 인벤토리 아이템
	/// </summary>
	[CreateAssetMenu(menuName = ("InventorySystem/Action Item"))]
    public class ActionItem : InventoryItem
    {
        [Tooltip("소모하는 아이템")]
        [SerializeField] bool consumable = false;

        /// <summary>
        /// 아이템 사용
        /// </summary>
        /// <param name="user">플레이어가 사용함<param>
        public virtual void Use(GameObject user)
        {
            Debug.Log("Using action: " + this);
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}