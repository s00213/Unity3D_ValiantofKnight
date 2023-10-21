using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	[SerializeField] private GameObject equipmentUI;
	[SerializeField] private bool isOpen;

	private void Start()
	{
		// 시작 시에는 장비창을 열지 않음
		isOpen = false;
	}

	void Update()
	{
		// 장비창 열고 닫는 단축키 : I
		if (Input.GetKeyDown(KeyCode.E) && !isOpen)
		{
			Debug.Log("E를 눌렀음, Open Equipment");
			equipmentUI.SetActive(true);
			isOpen = true;
		}
		else if (Input.GetKeyDown(KeyCode.E) && isOpen)
		{
			Debug.Log("E를 눌렀음, Close Equipment");
			equipmentUI.SetActive(false);
			isOpen = false;
		}
	}
}
