using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VillageScene : BaseScene
{
	protected override IEnumerator LoadingRoutine()
	{
		yield return new WaitForSecondsRealtime(1f);
	}
}
