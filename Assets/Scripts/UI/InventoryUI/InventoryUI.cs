using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventories;

namespace UI.Inventories
{
	/// <summary>
	/// 인벤토리 UI
	/// </summary>
	public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventorySlotUI InventoryItemPrefab = null;

		private InventorySystem playerInventory;

        private void Awake() 
        {
            playerInventory = InventorySystem.GetPlayerInventory();
            playerInventory.inventoryUpdated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < playerInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, transform);
                itemUI.Setup(playerInventory, i);
            }
        }
    }
}