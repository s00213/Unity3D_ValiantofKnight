using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class ItemData : ScriptableObject
{
	public int ID => id;
	public string ItemName => itmeName;
	public string Tooltip => tooltip;
	public Sprite IconSprite => iconSprite;

	[SerializeField] private int id;
	[SerializeField] private string itmeName;    // 아이템 이름
	[Multiline]
	[SerializeField] private string tooltip; // 아이템 설명
	[SerializeField] private Sprite iconSprite; // 아이템 아이콘
	[SerializeField] private GameObject dropItemPrefab; // 바닥에 떨어질 때 생성할 프리팹

	public abstract Item CreateItem();
}
