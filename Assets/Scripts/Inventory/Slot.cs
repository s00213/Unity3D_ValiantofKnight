using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private TMP_Text AmountText;

	private Item _item;
	public Item item
	{
		get { return _item; }
		set
		{
			_item = value;
			// AddItem()과 FreshSlot() 함수에서 사용
			// Inventory의 List<Item> items에 등록된 아이템이 있다면 itemImage를 image에 저장 그리고 Image의 알파 값을 1로 하여 이미지를 표시
			// 만약 item이 null 이면(빈슬롯 이면) Image의 알파 값 0을 주어 화면에 표시하지 않음
			if (_item != null)
			{
				image.sprite = item.itemIcon;
				image.color = new Color(1, 1, 1, 1);
			}
			else
			{
				image.color = new Color(1, 1, 1, 0);
			}
		}
	}

}
