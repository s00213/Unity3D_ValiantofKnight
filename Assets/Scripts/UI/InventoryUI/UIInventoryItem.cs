using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
	/// <summary>
	/// UIInventoryItem 클래스는 인벤토리 UI에서 개별 아이템 슬롯임
	/// </summary>
	public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] private Image itemImage; // 아이템 이미지
		[SerializeField] private TMP_Text quantityTxt; // 아이템 수량
		[SerializeField] private Image borderImage; // 선택된 아이템을 강조하기 위한 테두리 이미지

		// 아이템 이벤트를 처리하기 위한 델리게이트와 이벤트
		public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseButtonClick;

        private bool empty = true; // 아이템 슬롯이 비어있는지 여부 확인

		public void Awake()
        {
            ResetData();
            Deselect();
        }

		// 아이템 슬롯의 데이터를 초기화
		public void ResetData()
        {
			itemImage.gameObject.SetActive(false);
			empty = true;
        }

		// 아이템 슬롯의 선택을 해제
		public void Deselect()
        {
			// 테두리 이미지 비활성화
			borderImage.enabled = false;
        }

		// 아이템 슬롯에 데이터를 설정
		public void SetData(Sprite sprite, int quantity)
        {

            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            empty = false;
        }

		// 아이템 슬롯을 선택
		public void Select()
        {
			// 테두리 이미지 비활성화
			borderImage.enabled = true;
        }

		// 마우스 포인터 클릭 이벤트를 처리
		public void OnPointerClick(PointerEventData pointerData)
        {
			// 마우스 오른쪽 버튼을 클릭한 경우 오른쪽 마우스 클릭 이벤트 발생하고 그 외에는 일반 클릭 이벤트 발생
			if (pointerData.button == PointerEventData.InputButton.Right)
            {
				OnRightMouseButtonClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

		// 드래그가 끝났을 때 발생하는 이벤트를 처리
		public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

		// 드래그가 시작될 때 발생하는 이벤트를 처리
		public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

		// 다른 슬롯 위에 아이템이 드래그되었을 때 발생하는 이벤트를 처리
		public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

		// 아이템이 드래그되는 동안 지속적으로 발생하는 이벤트를 처리
		public void OnDrag(PointerEventData eventData) { }
    }
}