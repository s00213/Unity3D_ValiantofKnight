using System.Collections;
using System.Collections.Generic;
using UI.Tooltips;
using Quests;
using UnityEngine;

namespace UI.Quests
{
	public class QuestTooltipSpawner : TooltipSpawner
	{
		public override bool CanCreateTooltip()
		{
			return true;
		}

		public override void UpdateTooltip(GameObject tooltip)
		{
			QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();
			tooltip.GetComponent<QuestTooltipUI>().Setup(status);
		}
	}
}
