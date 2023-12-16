using Inventories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Rendering;
using UnityEngine;

namespace Dialog
{
	public class DialogManager : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI triggerText;
		[SerializeField] private GameObject dialogUI;
		[SerializeField] private TextMeshProUGUI dialogText;
		[SerializeField] private TextMeshProUGUI nPCNameText;

		[SerializeField, Multiline] private string npcName;
		[SerializeField] private string[] sentences;
		[SerializeField] private int currentSentence;

		public bool canAcitveBox;
		private DialogSystem dialogSystem;

		private void Update()
		{
			if (canAcitveBox && Input.GetKeyDown(KeyCode.E) && !DialogSystem.Dialog.isDialogBoxActive())
			{
				DialogSystem.Dialog.ActivateDialog(sentences);
				TalkToNPC();
			}
		}

		private void OnTriggerEnter(Collider Collison)
		{
			if (Collison.CompareTag("Player"))
			{
				canAcitveBox = true;
				//triggerText.gameObject.SetActive(true);
				//triggerText.text = npcName + " 와 대화하려면 [ E ] 키를 누르세요";
			}
		}

		private void OnTriggerExit(Collider Collison)
		{
			if (Collison.CompareTag("Player"))
			{
				canAcitveBox = false;
				//triggerText.gameObject.SetActive(true);
				//triggerText.text = npcName + " 와 대화하려면 [ E ] 키를 누르세요";
				//if (Collison.CompareTag("Player"))
				//{
				//	if (!isTriggered)
				//	{
				//		triggerText.text = "";
				//		triggerText.gameObject.SetActive(false);
				//		dialogUI.gameObject.SetActive(false);
				//	}			
			}
		}

		private void TalkToNPC()
		{
			//triggerText.text = "";
			//triggerText.gameObject.SetActive(false);
			dialogUI.gameObject.SetActive(true);
			DialogSystem.Dialog.SetNPCName(npcName);
			DialogSystem.Dialog.ActivateDialog(sentences);
		}
	}
}
