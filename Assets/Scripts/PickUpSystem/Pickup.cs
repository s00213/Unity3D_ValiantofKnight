using UnityEngine;

namespace Inventories
{
	/// <summary>
	/// 아이템 종류, 개수 등 픽업
	/// </summary>
	public class Pickup : MonoBehaviour
    {
		private InventoryItem item;
		private int number = 1;

		private InventorySystem inventory;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<InventorySystem>();
        }

		/// <summary>
		/// Prefab을 생성한 후 필수 데이터를 설정함
		/// </summary>
		/// <param name="item"> 항목 </param>
		/// <param name="number"> 수량 </param>
		public void Setup(InventoryItem item, int number)
        {
            this.item = item;
            if (!item.IsStackable())
            {
                number = 1;
            }
            this.number = number;
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public int GetNumber()
        {
            return number;
        }

        public void PickupItem(string itemName)
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(item, number);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
        }

        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(item);
        }
    }
}