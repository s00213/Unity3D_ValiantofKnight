using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

// 퀘스트 수행 상태
public enum TaskState
{
	Inactive,
	Running,
	Complete
}

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
	#region Events
	public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
	public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuccess);
	#endregion

	[SerializeField] private Category category;

	[Header("Text")]
	[SerializeField] public string codeName; // 코드 이름
	[SerializeField] private string description; // 설명

	[Header("Target")]
	[SerializeField] private TaskTarget[] targets; // 타겟이 여러 개일 경우 대비

	[Header("Action")]
	[SerializeField] private TaskAction action;

	[Header("Setting")]
	[SerializeField] private InitialSuccessValue initialSuccessValue;
	[SerializeField] private int needSuccessToComplete; // 필요한 성공 횟수
	[SerializeField] private bool canReceiveReportsDuringCompletion; // 퀘스트 완료했어도 계속 성공 횟수를 확인함

	private TaskState state;
	private int currentSuccess;

	public event StateChangedHandler onStateChanged;
	public event SuccessChangedHandler onSuccessChanged;

	// 성공한 횟수
	public int CurrentSuccess 
	{ 
		get => currentSuccess;
		set
		{
			int prevSuccess = currentSuccess;
			currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
			if (currentSuccess != prevSuccess)
			{
				State = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running;
				onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
			}
		}
	}
	public Category Category => category;
	public string CodeName => codeName;
	public string Description => description;
	public int NeedSuccessToComplete => needSuccessToComplete;

	public TaskState State
	{
		get => state;
		set
		{
			var prevState = state;
			state = value;
			onStateChanged?.Invoke(this, state, prevState);
		}

	}
	public bool IsComplete => State == TaskState.Complete;
	public Quest Owner { get; private set; }

	// Awake 역할
	public void Setup(Quest owner)
	{
		Owner = owner;
	}

	public void Start()
	{
		State = TaskState.Running;
		if (initialSuccessValue)
			CurrentSuccess = initialSuccessValue.GetValue(this);
	}

	public void End()
	{
		onStateChanged = null;
		onSuccessChanged = null;
	}

	// 외부에서 CurrentSuccess 값을 조작할 수 있음
	public void ReceiveReport(int successCount)
	{
		CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
	}

	public void Complete()
	{
		CurrentSuccess = needSuccessToComplete;
	}

	// 타겟인지 확인함
	public bool IsTarget(string category, object target)
		=> Category == category &&
		targets.Any(x => x.IsEqual(target)) &&
		(!IsComplete || (IsComplete && canReceiveReportsDuringCompletion));

	public bool ContainsTarget(object target) => targets.Any(x => x.IsEqual(target));
}


