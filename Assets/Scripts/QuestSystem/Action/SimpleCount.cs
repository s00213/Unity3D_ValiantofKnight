using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 단순히 현재 성공 카운트에 받은 성공 카운트를 더해서 전달함
/// </summary>
[CreateAssetMenu(menuName = "Quest/Task/Action/SimpleCount", fileName = "Simple Count")]
public class SimpleCount : TaskAction
{
	public override int Run(Task task, int currentSuccess, int successCount)
	{
		return currentSuccess + successCount;
	}
}
