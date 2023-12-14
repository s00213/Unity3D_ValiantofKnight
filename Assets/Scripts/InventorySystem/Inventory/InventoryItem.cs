using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Inventories
{
    /// <summary>
    /// 스크립터블 오브젝트
    /// </remarks>
    public abstract class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {
        [Tooltip("저장 또는 로드를 위해 자동 생성된 UUID")]
        [SerializeField] string itemID = null;
        [Tooltip("UI에 표시될 항목 이름")]
        [SerializeField] string displayName = null;
        [Tooltip("UI에 표시될 항목 설명")]
        [SerializeField][TextArea] string description = null;
        [Tooltip("인벤토리에서 이 아이템을 나타내는 UI 아이콘")]
        [SerializeField] Sprite icon = null;
        [Tooltip("아이템의 프리팹")]
		[SerializeField] Pickup pickUp = null;
        [Tooltip("true인 경우, 이 유형의 여러 항목을 동일한 인벤토리 슬롯에 쌓을 수 있음")]
        [SerializeField] bool stackable = false;

        static Dictionary<string, InventoryItem> itemLookupCache;

        public static InventoryItem GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }
        
        public Pickup SpawnPickup(Vector3 position, int number)
        {
            var pickup = Instantiate(this.pickUp);
            pickup.transform.position = position;
            pickup.Setup(this, number);
            return pickup;
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetItemID()
        {
            return itemID;
        }

        public bool IsStackable()
        {
            return stackable;
        }
        
        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
			// 비어 있으면 새 UUID를 생성하고 저장함
			if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
           
        }
    }
}
