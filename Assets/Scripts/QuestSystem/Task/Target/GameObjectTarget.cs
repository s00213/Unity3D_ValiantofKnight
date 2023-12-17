using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/GameObject", fileName = "Target_")]
public class GameObjectTarget : TaskTarget
{
	[SerializeField] private GameObject value;

	public override object Value => value;

	public override bool IsEqual(object target)
	{
		var targetAsGameObject = target as GameObject;
		if (targetAsGameObject == null)
			return false;
		// 프리팹 사용하면 이름이 바뀌기 때문에 생성된 Object의 원본이 프리팹의 이름이 포함되어 있는지 확인하여 같은 오브젝트인지 체크
		return targetAsGameObject.name.Contains(value.name);
	}
}
