using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public abstract class ItemData : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; set; }
        public int ID => GetInstanceID();
        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField, TextArea] public string Description { get; set; }
        [field: SerializeField] public Sprite ItemImage { get; set; }
		[field: SerializeField] public GameObject itemPrefab { get; set; }
		[field: SerializeField] public List<ItemParameter> DefaultParametersList { get; set; }
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterData itemParameter; // 매개변수 종류
		public float value; // 매개변수 값

		// 두 ItemParameter 구조체가 동일한지 비교
		public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}

