using System;
using UnityEngine;
using Saving;

namespace Inventories
{
    /// <summary>
    /// 인벤토리 시스템
    /// </summary>
    public class InventorySystem : MonoBehaviour, ISaveable
    {
        [Tooltip("Allowed size")]
        [SerializeField] int inventorySize = 16;

		private InventorySlot[] slots;

        public struct InventorySlot
        {
            public InventoryItem item;
            public int number;
        }

        public event Action inventoryUpdated;

        public static InventorySystem GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<InventorySystem>();
        }

        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        public int GetSize()
        {
            return slots.Length;
        }

		// <summary>
		/// 비어있는 첫 번째 슬롯에 항목을 추가함
		/// </summary>
		/// <param name="item"> 추가할 항목 </param>
		/// <param name="number"> 추가할 수량 </param>
		/// <returns> 항목을 추가할 수 있는지 여부를 반환함 </returns>
		public bool AddToFirstEmptySlot(InventoryItem item, int number)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i].item = item;
            slots[i].number += number;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

		/// <summary>
		/// 인벤토리에 해당 아이템이 있는지 확인
		/// </summary>
		public bool HasItem(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i].item, item))
                {
                    return true;
                }
            }
            return false;
        }

		/// <summary>
		/// 주어진 슬롯의 아이템 유형을 반환함
		/// </summary>
		public InventoryItem GetItemInSlot(int slot)
        {
            return slots[slot].item;
        }

		/// <summary>
		/// 주어진 슬롯에 있는 항목 수를 반환함
		/// </summary>
		public int GetNumberInSlot(int slot)
        {
            return slots[slot].number;
        }

		/// <summary>
		/// 주어진 슬롯에서 여러 항목을 제거함
		/// </summary>
		public void RemoveFromSlot(int slot, int number)
        {
            slots[slot].number -= number;
            if (slots[slot].number <= 0)
            {
                slots[slot].number = 0;
                slots[slot].item = null;
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }

        public bool AddItemToSlot(int slot, InventoryItem item, int number)
        {
            if (slots[slot].item != null)
            {
                return AddToFirstEmptySlot(item, number); ;
            }

            var i = FindStack(item);
            if (i >= 0)
            {
                slot = i;
            }

            slots[slot].item = item;
            slots[slot].number += number;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        private void Awake()
        {
            slots = new InventorySlot[inventorySize];
        }

		/// <summary>
		/// 비어있는 슬롯을 찾음
		/// </summary>
		/// <returns> 슬롯이 발견되지 않은 경우 -1을 반환함 </returns>
		private int FindSlot(InventoryItem item)
        {
            int i = FindStack(item);
            if (i < 0)
            {
                i = FindEmptySlot();
            }
            return i;
        }

		/// <summary>
		/// 빈 슬롯을 찾음
		/// </summary>
		/// <returns> 모든 슬롯이 가득 찬 경우 -1을 반환함 </returns>
		private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    return i;
                }
            }
            return -1;
        }

		/// <summary>
		/// 이 항목 유형의 기존 스택을 찾음
		/// </summary>
		/// <returns> 스택이 없거나 수량을 쌓을 수 없는 경우 -1을 반환함 </returns>
		private int FindStack(InventoryItem item)
        {
            if (!item.IsStackable())
            {
                return -1;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i].item, item))
                {
                    return i;
                }
            }
            return -1;
        }

		// 인벤토리 시스템 데이터 저장
		[System.Serializable]
        private struct InventorySlotRecord
        {
            public string itemID;
            public int number;
        }
    
        object ISaveable.CaptureState()
        {
            var slotStrings = new InventorySlotRecord[inventorySize];
            for (int i = 0; i < inventorySize; i++)
            {
                if (slots[i].item != null)
                {
                    slotStrings[i].itemID = slots[i].item.GetItemID();
                    slotStrings[i].number = slots[i].number;
                }
            }
            return slotStrings;
        }

        void ISaveable.RestoreState(object state)
        {
            var slotStrings = (InventorySlotRecord[])state;
            for (int i = 0; i < inventorySize; i++)
            {
                slots[i].item = InventoryItem.GetFromID(slotStrings[i].itemID);
                slots[i].number = slotStrings[i].number;
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }
    }
}