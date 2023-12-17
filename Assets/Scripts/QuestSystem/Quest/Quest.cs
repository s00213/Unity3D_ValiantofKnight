using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public enum QuestState
{
	Inactive,
	Running,
	Complete,
	Cancel,
	WaitingForCompletion // 플레이어가 퀘스트 완료 처리를 해야함
}

[CreateAssetMenu(menuName = "Quest/Quest", fileName = "Quest_")]
public class Quest : ScriptableObject
{
	#region Events
	public delegate void TaskSuccessChangedHandler(Quest quest, Task task, int currentSuccess, int prevSuccess);
	public delegate void CompletedHandler(Quest quest);
	public delegate void CanceledHandler(Quest quest);
	public delegate void NewTaskGroupHandler(Quest quest, TaskGroup currentTaskGroup, TaskGroup prevTaskGroup);
	#endregion

	[SerializeField] private Category category;
	[SerializeField] private Sprite icon;

	[Header("Text")]
	[SerializeField] private string codeName;
	[SerializeField] private string displayName;
	[SerializeField, TextArea] private string description;

	[Header("Task")]
	[SerializeField] private TaskGroup[] taskGroups;

	[Header("Reward")]
	[SerializeField] private Reward[] rewards;

	[Header("Option")]
	[SerializeField] private bool useAutoComplete; // 자동으로 완료 처리
	[SerializeField] private bool isCancelable;
	[SerializeField] private bool isSavable;

	[Header("Condition")]
	[SerializeField] private Condition[] acceptionConditions;
	[SerializeField] private Condition[] cancelConditions;

	private int currentTaskGroupIndex;

	public Category Category => category;
	public Sprite Icon => icon;
	public string CodeName => codeName;
	public string DisplayName => displayName;
	public string Description => description;
	public QuestState State { get; private set; }
	public TaskGroup CurrentTaskGroup => taskGroups[currentTaskGroupIndex];
	public IReadOnlyList<TaskGroup> TaskGroups => taskGroups;
	public IReadOnlyList<Reward> Rewards => rewards;
	public bool IsRegistered => State != QuestState.Inactive;
	public bool IsComplatable => State == QuestState.WaitingForCompletion;
	public bool IsComplete => State == QuestState.Complete;
	public bool IsCancel => State == QuestState.Cancel;
	public virtual bool IsCancelable => isCancelable && cancelConditions.All(x => x.IsPass(this));
	public bool IsAcceptable => acceptionConditions.All(x => x.IsPass(this));
	public virtual bool	IsSavable => isSavable;

	public event TaskSuccessChangedHandler onTaskSuccessChanged;
	public event CompletedHandler onCompleted;
	public event CanceledHandler onCanceled;
	public event NewTaskGroupHandler onNewTaskGroup;

	// 퀘스트가 등록되었을 때 실행됨, Awake 역할의 함수
	public void OnRegister()
	{
		Debug.Assert(!IsRegistered, "This quest has already been registered.");

		foreach (var taskGroup in taskGroups)
		{
			taskGroup.Setup(this);
			foreach (var task in taskGroup.Tasks)
				task.onSuccessChanged += OnSuccessChanged;
		}

		State = QuestState.Running;
		CurrentTaskGroup.Start();
	}

	// 퀘스트 상태 보고 받는 함수
	public void ReceiveReport(string category, object target, int successCount)
	{
		Debug.Assert(IsRegistered, "This quest has already been registered.");
		Debug.Assert(!IsCancel, "This quest has been canceled.");

		if (IsComplete)
			return;

		CurrentTaskGroup.ReceiveReport(category, target, successCount);

		if (CurrentTaskGroup.IsAllTaskComplete)
		{
			// 다음 Task 그룹이 없다면,
			if (currentTaskGroupIndex + 1 == taskGroups.Length)
			{
				State = QuestState.WaitingForCompletion;
				if (useAutoComplete)
					Complete();
			}
			// 다음 Task 그룹이 존재한다면,
			else
			{				
				var prevTasKGroup = taskGroups[currentTaskGroupIndex++];
				prevTasKGroup.End();
				CurrentTaskGroup.Start();
				onNewTaskGroup?.Invoke(this, CurrentTaskGroup, prevTasKGroup);
			}
		}
		else
			State = QuestState.Running;
	}

	// 퀘스트 완료
	public void Complete()
	{
		CheckIsRunning();

		foreach (var taskGroup in taskGroups)
			taskGroup.Complete();

		State = QuestState.Complete;

		foreach (var reward in rewards)
			reward.Give(this);

		onCompleted?.Invoke(this);

		onTaskSuccessChanged = null;
		onCompleted = null;
		onCanceled = null;
		onNewTaskGroup = null;
	}

	// 퀘스트 취소
	public virtual void Cancel()
	{
		CheckIsRunning();
		Debug.Assert(IsCancelable, "This quest can't be canceled");

		State = QuestState.Cancel;
		onCanceled?.Invoke(this);
	}

	// 타겟을 가진 Task가 있는지 확인
	public bool ContainsTarget(object target) => taskGroups.Any(x => x.ContainsTarget(target));

	public bool ContainsTarget(TaskTarget target) => ContainsTarget(target.Value);

	public Quest Clone()
	{
		var clone = Instantiate(this);
		clone.taskGroups = taskGroups.Select(x => new TaskGroup(x)).ToArray();

		return clone;
	}

	// 퀘스트 데이터 저장
	public QuestSaveData ToSaveData()
	{
		return new QuestSaveData
		{
			codeName = CodeName,
			state = State,
			taskGroupIndex = currentTaskGroupIndex,
			taskSuccessCounts = CurrentTaskGroup.Tasks.Select(x => x.CurrentSuccess).ToArray()
		};
	}

	// 퀘스트 데이터 로드
	public void LoadFrom(QuestSaveData saveData)
	{ 
		State = saveData.state;
		currentTaskGroupIndex = saveData.taskGroupIndex;

		for (int i = 0; i < currentTaskGroupIndex; i++)
		{
			var taskGroup = taskGroups[i];
			taskGroup.Start();
			taskGroup.Complete();
		}

		for (int i = 0; i < saveData.taskSuccessCounts.Length; i++)
		{
			CurrentTaskGroup.Start();
			CurrentTaskGroup.Tasks[i].CurrentSuccess = saveData.taskSuccessCounts[i];
		}
	}

	// 콜백 함수
	private void OnSuccessChanged(Task task, int currentSuccess, int prevSuccess)
		=> onTaskSuccessChanged?.Invoke(this, task, currentSuccess, prevSuccess);

	[Conditional("UNITY_EDITOR")]
	private void CheckIsRunning()
	{
		Debug.Assert(IsRegistered, "This quest has already been registered");
		Debug.Assert(!IsCancel, "This quest has been canceled.");
		Debug.Assert(!IsComplete, "This quest has already been completed");
	}
}
