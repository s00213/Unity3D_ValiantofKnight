using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
	public Transform targetTransform; // 따라가야 할 대상을 연결할 변수								  
	[Range(2.0f, 20.0f)] public float distance = 10.0f; // 따라갈 대상으로부터 떨어질 거리	
	[Range(0.0f, 10.0f)] public float height = 2.0f; // Y축으로 이동할 높이	
	public float damping = 10.0f; // 반응 속도	
	public float targetOffset = 2.0f; // 카메라 LookAt의 Offset 값

	private Transform camTransform;
	private Vector3 velocity = Vector3.zero;

	void Start()
	{
		camTransform = GetComponent<Transform>();
	}

	void LateUpdate()
	{
		// 추적해야 할 대상의 뒤쪽으로 distance만큼 이동 + 높이를 height만큼 이동 
		Vector3 pos = targetTransform.position + (-targetTransform.forward * distance) + (Vector3.up * height);

		// SmoothDamp을 이용한 위치 보간 (시작 위치, 목표 위치, 현재 속도, 목표 위치까지 도달할 시간)
		camTransform.position = Vector3.SmoothDamp(camTransform.position, pos, ref velocity, damping);

		// Camera를 피벗 좌표를 향해 회전
		camTransform.LookAt(targetTransform.position + (targetTransform.up * targetOffset));
	}
}
