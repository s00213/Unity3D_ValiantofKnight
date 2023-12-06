using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct InventoryItem
{
	public int quantity; // 아이템 수량
	public ItemData item; // 아이템 데이터
	public List<ItemParameter> itemState; // 아이템 상태 리스트
	public bool IsEmpty => item == null; // 아이템이 비어있는지 여부

	// 수량을 변경한 새로운 인벤토리 아이템 반환
	public InventoryItem ChangeQuantity(int newQuantity)
	{
		return new InventoryItem
		{
			item = this.item,
			quantity = newQuantity,
			itemState = new List<ItemParameter>(this.itemState)
		};
	}

	// 빈 인벤토리 아이템 반환
	public static InventoryItem GetEmptyItem() => new InventoryItem
	{
		item = null,
		quantity = 0,
		itemState = new List<ItemParameter>()
	};
}

namespace Inventory
{
    [CreateAssetMenu]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems; // 인벤토리 아이템 리스트
		[field: SerializeField] public int Size { get; private set; } // 인벤토리 크기

		// 인벤토리 갱신 이벤트
		public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

		// 인벤토리 초기화
		public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

		// 아이템 추가
		public int AddItem(ItemData item, int quantity, List<ItemParameter> itemState = null)
        {
			// 중첩 불가능한 아이템인 경우
			if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
					// 남은 수량이 있고, 인벤토리가 가득 차지 않았을 때
					while (quantity > 0 && IsInventoryFull() == false)
                    {
						// 빈 슬롯에 아이템을 추가하고, 추가한 수량을 뺌
						quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                    }
                    InformAboutChange();
					return quantity;
                }
            }
			// 중첩 가능한 아이템인 경우
			quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

		// 첫 번째 빈 슬롯에 아이템 추가
		private int AddItemToFirstFreeSlot(ItemData item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = 
                new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

		// 인벤토리가 가득 찼는지 확인
		private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

		// 중첩 가능한 아이템 추가
		private int AddStackableItem(ItemData item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if(inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
						// 중첩 가능한 최대 수량까지 아이템을 추가하고 남은 수량을 계산함
						inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
						// 남은 수량이 중첩 가능한 최대 수량보다 작은 경우 중첩된 수량을 갱신하고 종료함
						inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
			// 아직 남은 수량이 있다면, 빈 슬롯에 중첩 가능한 최대 수량까지 아이템을 추가함
			while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);

                InformAboutChange();
            }
        }

		// 인벤토리 아이템 추가
		public void AddItem(InventoryItem item)
        {
			// AddItem 메서드를 통해 아이템 추가
			AddItem(item.item, item.quantity);
        }

		// 현재 인벤토리 상태 반환
		public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

			// 인벤토리를 순회하면서 비어있지 않은 슬롯 정보를 반환값에 추가
			for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

		// 특정 인덱스의 인벤토리 아이템 반환
		public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

		// 두 아이템 슬롯 교환
		public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
			// 첫 번째 아이템 슬롯을 임시로 저장
			InventoryItem item1 = inventoryItems[itemIndex_1];
			// 두 아이템 슬롯을 교환
			inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

		// 인벤토리 갱신 이벤트 호출
		private void InformAboutChange()
        {
		    // 등록된 이벤트 리스너에게 현재 인벤토리 상태를 알림
			OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
}
