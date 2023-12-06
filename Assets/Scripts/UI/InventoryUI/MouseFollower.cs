using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas; // UI 캔버스
	[SerializeField] private UIInventoryItem item; // UI 인벤토리 아이템

	public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
		// UI 인벤토리 아이템에 데이터 설정
		item.SetData(sprite, quantity);
    }

    void Update()
    {
		// 마우스 위치를 화면 좌표에서 캔버스 로컬 좌표로 변환
		Vector2 position;
		// 캔버스의 RectTransform, 현재 마우스 위치, 캔버스의 월드 카메라
		RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
		// 변환된 로컬 좌표를 월드 좌표로 변환하여 마우스 팔로워의 위치로 설정
		transform.position = canvas.transform.TransformPoint(position);
    }

	// 활성화/비활성화 토글 함수
	public void Toggle(bool value)
    {
		// 활성화 여부에 따라 게임 오브젝트 활성화/비활성화
		Debug.Log($"Item toggled {value}");
        gameObject.SetActive(value);
    }
}
