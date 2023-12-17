using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskTarget : ScriptableObject
{
	public abstract object Value { get; }

	// 퀘스트 시스템에 보고된 타겟이 Task에 설정한 Target과 같은지 확인함
	public abstract bool IsEqual(object target);
}

