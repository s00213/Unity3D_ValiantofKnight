using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
	[SerializeField] private GameObject triggerText;
	[SerializeField] private GameObject dialogUI;
	[SerializeField] private bool canAtivateBox;

	[SerializeField, Multiline] private string npcName;
	[SerializeField] private string[] sentences;
	[SerializeField] private int currentSentence;

	private void Update()
	{
		TalkToNPC();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			triggerText.SetActive(true);
			canAtivateBox = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			triggerText.SetActive(false);
			canAtivateBox = false;
			dialogUI.SetActive(false);
		}
	}

	private void TalkToNPC()
	{	
		if (canAtivateBox && Input.GetKeyDown(KeyCode.C))
		{
			dialogUI.SetActive(true);
			canAtivateBox = true;
			DialogSystem.Dialog.SetNPCName(npcName);
			DialogSystem.Dialog.ActivateDialog(sentences);
		}
	}
}
