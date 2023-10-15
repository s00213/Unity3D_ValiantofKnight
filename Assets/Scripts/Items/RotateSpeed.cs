using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpeed : MonoBehaviour
{
	[SerializeField] private float rotateSpeed;

	private void Update()
	{
		// 아이템 회전
		transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
	}
}
