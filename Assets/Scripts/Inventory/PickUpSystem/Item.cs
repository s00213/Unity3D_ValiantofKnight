using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
	[field: SerializeField] public ItemSO InventoryItem { get; private set; }
	[field: SerializeField] public int Quantity { get; set; } = 1;
	
	[SerializeField] private AudioSource audioSource;

	public TMP_Text triggerText;
	public string itemName;

	private bool isTriggered;

	private void OnTriggerStay(Collider other)
	{
		triggerText.gameObject.SetActive(true);
		triggerText.text = "아이템을 획득하려면 [ E ] 키를 누르세요" + name;
		if (Input.GetKeyDown(KeyCode.E))
		{
			DestroyItem();
			triggerText.gameObject.SetActive(false);
			isTriggered = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!isTriggered)
		{
			triggerText.text = "";
			triggerText.gameObject.SetActive(false);
		}
	}

	public void DestroyItem()
	{
		audioSource.Play();
		Destroy(gameObject);
	}
}
