using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UI.Inventories;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI triggerText;
	[SerializeField] private GameObject dialogUI;
	[SerializeField] private TextMeshProUGUI dialogText;
	[SerializeField] private TextMeshProUGUI nPCNameText;
	[SerializeField] private GameObject quickSlots;

	[SerializeField, Multiline] private string npcName;
	[SerializeField] private string[] sentences;
	[SerializeField] private int currentSentence;

	private bool isTriggered;

	// 대화창 열릴 때 퀵슬롯 사라지게 함
	public static event Action OnConversationStarted;
	public static event Action OnConversationEnded;

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			triggerText.gameObject.SetActive(true);
			triggerText.text = npcName + " 와 대화하려면 [ E ] 키를 누르세요";
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!isTriggered)
			{
				triggerText.text = "";
				triggerText.gameObject.SetActive(false);
				dialogUI.gameObject.SetActive(false);
				OnConversationEnded?.Invoke();
			}			
		}
	}

	private void Update()
	{
		TalkToNPC();		
	}

	private void TalkToNPC()
	{
		if (isTriggered && Input.GetKeyDown(KeyCode.E))
		{
			triggerText.text = "";
			triggerText.gameObject.SetActive(false);
			dialogUI.gameObject.SetActive(true);

			OnConversationStarted?.Invoke();
			DialogSystem.Dialog.SetNPCName(npcName);
			DialogSystem.Dialog.ActivateDialog(sentences);
		}
	}
}
