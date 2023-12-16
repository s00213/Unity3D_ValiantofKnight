using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialog
{
	public class DialogSystem : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI dialogText;
		[SerializeField] private TextMeshProUGUI nPCNameText;
		[SerializeField] private GameObject dialogUI;
		[SerializeField] private GameObject nPCNameBox;

		[SerializeField] private string[] dialogSentences;
		[SerializeField] private int currentSentence;

		public static DialogSystem Dialog;

		private void Awake()
		{
			if (Dialog != null)
			{
				Destroy(this);
				return;
			}

			Dialog = this;
			DontDestroyOnLoad(this);
		}

		private void OnDestroy()
		{
			if (Dialog == this)
				Dialog = null;
		}

		private void Start()
		{
			dialogUI.SetActive(false);
			dialogText.text = dialogSentences[currentSentence];
		}

		private void Update()
		{
			PushText();
		}

		public void PushText()
		{
			if (dialogUI.activeInHierarchy)
			{
				if (Input.GetMouseButtonDown(0))
				{

					currentSentence++;

					if (currentSentence >= dialogSentences.Length)
					{
						dialogUI.SetActive(false);
					}
					else
					{
						dialogText.text = dialogSentences[currentSentence];
					}

				}
			}
		}

		public void SetNPCName(string newNPCName)
		{
			nPCNameText.text = newNPCName;
			nPCNameBox.SetActive(!string.IsNullOrEmpty(newNPCName));
		}

		public void ActivateDialog(string[] newSentenceToUse)
		{
			dialogSentences = newSentenceToUse;
			currentSentence = 0;

			dialogText.text = dialogSentences[currentSentence];
			dialogUI.SetActive(true);
		}

		public bool isDialogBoxActive()
		{
			return dialogUI.activeInHierarchy;
		}
	}
}
