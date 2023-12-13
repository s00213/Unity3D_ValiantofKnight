using UnityEngine;
using Inventories;
using TMPro;

namespace Control
{
	[RequireComponent(typeof(Pickup))]
	public class PickUpItem : MonoBehaviour//, IRaycastable
	{
		private Pickup pickup;
		private InventorySystem inventorySystem;

		public TMP_Text triggerText;
		public string itemName;
		private bool isTriggered;

		private void Awake()
		{
			pickup = GetComponent<Pickup>();
			
			triggerText = GetComponentInChildren<TMP_Text>();
			if (triggerText == null)
			{
				Debug.LogError("TMP_Text component not found.");
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (isTriggered) 
				return;

			triggerText.gameObject.SetActive(true);
			triggerText.text = "아이템을 획득하려면 [ E ] 키를 누르세요 " + itemName;	
			isTriggered = true;
		}

		private void OnTriggerExit(Collider other)
		{
			if (!isTriggered)
			{
				triggerText.text = "";
				triggerText.gameObject.SetActive(false);
			}
		}

		private void Update()
		{
			if (isTriggered && Input.GetKeyDown(KeyCode.E))
			{
				if (pickup.CanBePickedUp())
				{
					pickup.PickupItem(itemName);
					Destroy(gameObject);
				}
				else
				{
					Debug.Log("인벤토리가 꽉 찼습니다.");
				}
			}
			
		}
	}
}