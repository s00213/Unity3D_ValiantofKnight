using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Inventory : MonoBehaviour
{
	public List<Item> itemsList; // 인벤토리에 담을 리스트
	public Slot[] slots;

	[SerializeField] private GameObject inventoryUI;
	public static bool invectoryActivated = false;  // 인벤토리 활성화 여부

	[SerializeField] private Transform slotParent; // Slot의 부모 오브젝트

#if UNITY_EDITOR //  유니티 에디터에서 바로 작동을 하는 역할
	// 처음 인벤토리에 소스를 등록하면 Console창에 에러가 뜨지만 slots에 Slot들이 자동 등록됨
	private void OnValidate()
	{
		slots = slotParent.GetComponentsInChildren<Slot>();
	}
#endif

	public static Inventory instance;

    void Awake()
    {
		if (instance != null)
		{
			Destroy(this);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);	
	}

	private void OnDestroy()
	{
		if (instance == this)
			instance = null;
	}

	private void Start()
	{
		invectoryActivated = false;
		FreshSlot();
	}

	public void Update()
	{
		// 인벤토리 시스템 열고 닫는 단축키 : I
		if (Input.GetKeyDown(KeyCode.I))
		{
			if (invectoryActivated)
			{
				Debug.Log("I를 눌렀음, Close Inventory");
				inventoryUI.SetActive(false);
				invectoryActivated = false;
			}
			else
			{
				Debug.Log("I를 눌렀음, Open Inventory");
				inventoryUI.SetActive(true);
				invectoryActivated = true;
			}
		}
	}

	//  아이템이 들어오거나 나가면 Slot의 내용을 다시 정리하여 화면에 보여줌
	public void FreshSlot()
	{
		int i = 0;
		for (; i < itemsList.Count && i < slots.Length; i++)
		{
			slots[i].item = itemsList[i];
		}
		for (; i < slots.Length; i++)
		{
			slots[i].item = null;
		}
	}

	// 아이템 추가
	public void AddItem(Item _item)
	{
		print(_item.ToString());
		if (itemsList.Count < slots.Length)
		{
			itemsList.Add(_item);
			FreshSlot();
		}
		else
		{
			Debug.Log("슬롯이 가득 차 있습니다.");
		}
	}

    // 아이템 제거
    public void RemoveItem(Item _item)
    {
		itemsList.Remove(_item);
		FreshSlot();
	}  
}
