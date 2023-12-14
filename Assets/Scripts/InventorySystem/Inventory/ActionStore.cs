using System;
using System.Collections.Generic;
using UnityEngine;
using Saving;

namespace Inventories
{
    /// <summary>
    /// 퀵 슬롯
    /// </summary>
    public class ActionStore : MonoBehaviour, ISaveable
    {
        Dictionary<int, DockedItemSlot> dockedItems = new Dictionary<int, DockedItemSlot>();
        private class DockedItemSlot 
        {
            public ActionItem item;
            public int number;
        }

		/// <summary>
		/// 슬롯의 아이템이 추가 또는 제거될 때 이벤트가 발생됨
		/// </summary>
		public event Action storeUpdated;

        public ActionItem GetAction(int index)
        {
            if (dockedItems.ContainsKey(index))
            {
                return dockedItems[index].item;
            }
            return null;
        }

		/// <summary>
		/// 인덱스에 남아있는 항목 수를 확인함
		/// </summary>
		/// <returns>
		/// 인덱스에 항목이 없거나 항목이 있으면 0을 반환하는 경우는 완전히 소비함
		/// </returns>
		public int GetNumber(int index)
        {
            if (dockedItems.ContainsKey(index))
            {
                return dockedItems[index].number;
            }
            return 0;
        }

		/// <summary>
		/// 주어진 인덱스에 항목을 추가함
		/// </summary>
		/// <param name="item"> 항목 </param>
		/// <param name="index"> 추가할 위치 </param>
		/// <param name="number"> 추가할 수량 </param>
		public void AddAction(InventoryItem item, int index, int number)
        {
            if (dockedItems.ContainsKey(index))
            {  
                if (object.ReferenceEquals(item, dockedItems[index].item))
                {
                    dockedItems[index].number += number;
                }
            }
            else
            {
                var slot = new DockedItemSlot();
                slot.item = item as ActionItem;
                slot.number = number;
                dockedItems[index] = slot;
            }
            if (storeUpdated != null)
            {
                storeUpdated();
            }
        }

		/// <summary>
		/// 해당 슬롯에 있는 아이템을 사용하고, 
		/// 소모품인 경우 항목이 완전히 제거될 때까지 인스턴스는 파괴됨
		/// </summary>
		/// <param name="user">.</param>
		/// <returns> 실행 불가면 false </returns>
		public bool Use(int index, GameObject user)
        {
            if (dockedItems.ContainsKey(index))
            {
                dockedItems[index].item.Use(user);
                if (dockedItems[index].item.isConsumable())
                {
                    RemoveItems(index, 1);
                }
                return true;
            }
            return false;
        }

		/// <summary>
		/// 주어진 수의 항목을 제거함
		/// </summary>
		public void RemoveItems(int index, int number)
        {
            if (dockedItems.ContainsKey(index))
            {
                dockedItems[index].number -= number;
                if (dockedItems[index].number <= 0)
                {
                    dockedItems.Remove(index);
                }
                if (storeUpdated != null)
                {
                    storeUpdated();
                }
            }
            
        }

		/// <summary>
		/// 이 슬롯에 허용되는 최대 항목 수
		/// 
		/// 슬롯에 이미 항목이 포함되어 있는지 여부를 고려하고,
		/// 동일한 유형인지 여부 확인하고 같은 항목인 경우만 쌓기 허용하고,
		/// 아이템은 소모품임
		/// </summary>
		/// <returns> 유효한 경계가 없으면 int.MaxValue를 반환함 </returns>
		public int MaxAcceptable(InventoryItem item, int index)
        {
            var actionItem = item as ActionItem;
            if (!actionItem) return 0;

            if (dockedItems.ContainsKey(index) && !object.ReferenceEquals(item, dockedItems[index].item))
            {
                return 0;
            }
            if (actionItem.isConsumable())
            {
                return int.MaxValue;
            }
            if (dockedItems.ContainsKey(index))
            {
                return 0;
            }

            return 1;
        }

        // 데이터 저장
        [System.Serializable]
        private struct DockedItemRecord
        {
            public string itemID;
            public int number;
        }

        object ISaveable.CaptureState()
        {
            var state = new Dictionary<int, DockedItemRecord>();
            foreach (var pair in dockedItems)
            {
                var record = new DockedItemRecord();
                record.itemID = pair.Value.item.GetItemID();
                record.number = pair.Value.number;
                state[pair.Key] = record;
            }
            return state;
        }

        void ISaveable.RestoreState(object state)
        {
            var stateDict = (Dictionary<int, DockedItemRecord>)state;
            foreach (var pair in stateDict)
            {
                AddAction(InventoryItem.GetFromID(pair.Value.itemID), pair.Key, pair.Value.number);
            }
        }
    }
}