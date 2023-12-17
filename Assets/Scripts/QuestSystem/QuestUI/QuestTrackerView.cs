using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestTrackerView : MonoBehaviour
{
	[SerializeField] private QuestTracker questTrackerPrefab;
	[SerializeField] private CategoryColor[] categoryColors;

	private void Start()
	{
		QuestSystem.Instance.onQuestRegistered += CreateQuestTracker;

		foreach (var quest in QuestSystem.Instance.ActiveQuests)
			CreateQuestTracker(quest);
	}

	//  이벤트 등록 해제
	private void OnDestroy()
	{
		if (QuestSystem.Instance)
			QuestSystem.Instance.onQuestRegistered -= CreateQuestTracker;
	}

	private void CreateQuestTracker(Quest quest)
	{
		var categoryColor = categoryColors.FirstOrDefault(x => x.category == quest.Category);
		var color = categoryColor.category == null ? Color.white : categoryColor.color;
		Instantiate(questTrackerPrefab, transform).Setup(quest, color);
	}

	// 카테고리 별로 색 다르게 함
	[System.Serializable]
	private struct CategoryColor
	{
		public Category category;
		public Color color;
	}
}
