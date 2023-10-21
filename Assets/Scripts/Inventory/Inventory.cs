using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private GameObject inventoryUI;
	[SerializeField] private bool isOpen;

	private void Start()
	{
		// 시작 시에는 인벤토리를 열지 않음
		isOpen = false;
	}

	void Update()
	{
		// 인벤토리 열고 닫는 단축키 : I
		if (Input.GetKeyDown(KeyCode.I) && !isOpen)
		{
			Debug.Log("I를 눌렀음, Open Inventory");
			inventoryUI.SetActive(true);
			isOpen = true;
		}
		else if (Input.GetKeyDown(KeyCode.I) && isOpen)
		{
			Debug.Log("I를 눌렀음, Close Inventory");
			inventoryUI.SetActive(false);
			isOpen = false;
		}
	}
}
