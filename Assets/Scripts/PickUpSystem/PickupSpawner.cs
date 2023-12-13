using UnityEngine;
using Saving;

namespace Inventories
{
	/// <summary>
	/// 인벤토리 아이템에 대한 올바른 프리팹을 자동으로 생성함
	/// </summary>
	public class PickupSpawner : MonoBehaviour, ISaveable
    {
        [SerializeField] private InventoryItem item = null;
        [SerializeField] private int number = 1;

        private void Awake()
        {
            SpawnPickup();
        }

		/// <summary>
		/// 존재하는 경우 이 클래스에 의해 생성된 픽업을 반환함
		/// </summary>
		/// <returns> 픽업이 수집된 경우 null을 반환함 </returns>
		public Pickup GetPickup() 
        { 
            return GetComponentInChildren<Pickup>();
        }

		/// <summary>
		/// 픽업이 수집된 경우 true
		/// </summary>
		public bool isCollected() 
        { 
            return GetPickup() == null;
        }

        private void SpawnPickup()
        {
            var spawnedPickup = item.SpawnPickup(transform.position, number);
            spawnedPickup.transform.SetParent(transform);
        }

        private void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }

        // 저장
        object ISaveable.CaptureState()
        {
            return isCollected();
        }

        void ISaveable.RestoreState(object state)
        {
            bool shouldBeCollected = (bool)state;

            if (shouldBeCollected && !isCollected())
            {
                DestroyPickup();
            }

            if (!shouldBeCollected && isCollected())
            {
                SpawnPickup();
            }
        }
    }
}