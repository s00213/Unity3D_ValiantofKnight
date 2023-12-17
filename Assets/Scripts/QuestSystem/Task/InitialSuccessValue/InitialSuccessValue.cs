using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 퀘스트의 초기 성공 값을 지정
public abstract class InitialSuccessValue : ScriptableObject
{
	public abstract int GetValue(Task task);
}
