using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkerCompleted;

	private void Start()
	{
		questMarkerCompleted = new bool[questNames.Length];
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log(CheckIfComplete("Äù½ºÆ®1"));
			MarkQuestComplete("Äù½ºÆ®2");
			MarkQuestIncomplete("Äù½ºÆ®3");
		}
	}

	public int GetQuestNumber(string questToFind)
	{
		for (int i = 0; i < questNames.Length; i++)
		{
			if (questNames[i] == questToFind)
			{ 
				return i;
			}
		}

		Debug.LogWarning("Quest: " + questToFind + "dose not exist");
		return 0;
	}

	public bool CheckIfComplete(string questToCheck)
	{ 
		int questNumberToCheck = GetQuestNumber(questToCheck);

		if (questNumberToCheck != 0)
		{
			return questMarkerCompleted[questNumberToCheck];
		}

		return false;
	}

	public void MarkQuestComplete(string questToMark)
	{
		int questNumberToCheck = GetQuestNumber(questToMark);
		questMarkerCompleted[questNumberToCheck] = true;
	}

	public void MarkQuestIncomplete(string questToMark)
	{
		int questNumberToCheck = GetQuestNumber(questToMark);
		questMarkerCompleted[questNumberToCheck] = false;
	}
}
