using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
	[field: SerializeField]
	public bool isStackable { get; set; }

	public int ID => GetInstanceID();

	[field: SerializeField]
	public int maxStackSize { get; set; } = 1;

	[field: SerializeField]
	public string Name { get; set; }

	[field: SerializeField]
	[field: TextArea]
	public string description { get; set; }

	[field: SerializeField]
	public Sprite itemImage { get; set; }

	[field: SerializeField]
	public GameObject itemPrefab { get; set; }
}
