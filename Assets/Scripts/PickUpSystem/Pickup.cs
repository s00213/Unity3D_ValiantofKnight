using UnityEngine;

namespace Inventories
{
	/// <summary>
	/// 아이템 종류, 개수 등 픽업 데이터
	/// </summary>
	public class Pickup : MonoBehaviour
    {
        InventoryItem item;
        int number = 1;

		InventorySystem inventory;

		//[SerializeField] private QuestReporter questReporter;

		private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<InventorySystem>();
        }

		/// <summary>
		/// 프리팹 생성 후 필수 데이터를 설정
		/// </summary>
		/// <param name="item"> 프리팹이 나타내는 항목 유형 </param>
		/// <param name="number"> 항목 수 </param>
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

        public void PickupItem()
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