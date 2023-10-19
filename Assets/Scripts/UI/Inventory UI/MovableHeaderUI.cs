using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableHeaderUI : MonoBehaviour, IDragHandler, IPointerDownHandler
{
	[SerializeField] private Transform tartgetTransform; // UI

	private Vector2 startPoint;
	private Vector2 movePoint;

	private void Awake()
	{
		// 이동 대상 UI를 지정하지 않은 경우, 자동으로 부모로 초기화
		if (tartgetTransform == null)
		tartgetTransform = transform.parent;
	}

	// 드래그 : 마우스 커서 위치로 이동
	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		tartgetTransform.position = startPoint + (eventData.position - movePoint);
	}

	// 드래그 시작 위치 지정
	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
		startPoint = tartgetTransform.position;
		movePoint = eventData.position;
	}
}
