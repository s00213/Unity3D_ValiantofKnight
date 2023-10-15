using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonsterOrbit : MonoBehaviour
{
	/// <summary>
	/// 공전 타겟
	/// </summary>
	[Header("Target")]
	[SerializeField] private Transform orbitTarget;
	/// <summary>
	/// 공전 스피드
	/// </summary>
	[Header("Speed")]
	[SerializeField] private float orbitSpeed;
	///  <summary>
	/// 목표와의 거리(고정값)
	/// </summary>
	private Vector3 offSet;

	private void Start()
	{
		offSet = transform.position - orbitTarget.position;
	}

	private void Update()
	{
		transform.position = orbitTarget.position + offSet;

		// RotateAround : 타겟 주위를 회전하는 함수
		// -> 후의 위치를 가지고 목표와의 거리를 유지
		transform.RotateAround(orbitTarget.position, Vector3.up, orbitSpeed * Time.deltaTime);
		offSet = transform.position - orbitTarget.position;
	}
}
