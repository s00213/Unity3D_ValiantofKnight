using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Condition/QuestIsComplete", fileName = "IsQuestIsComplete_")]
public class QuestIsComplete : Condition
{
    [SerializeField] private Quest target;

	public override bool IsPass(Quest quest)
		=> QuestSystem.Instance.ContainsInCompleteQuests(target);
}
