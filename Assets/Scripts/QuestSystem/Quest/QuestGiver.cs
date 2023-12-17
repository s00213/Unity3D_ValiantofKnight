using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
	// 등록할 퀘스트
	[SerializeField] private Quest[] quests;

	private void Start()
	{
		foreach (var quest in quests)
		{
			if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompleteQuests(quest))
				QuestSystem.Instance.Register(quest);
		}
	}

}

