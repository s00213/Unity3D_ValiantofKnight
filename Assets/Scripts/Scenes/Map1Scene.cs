using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map1Scene : BaseScene
{
	protected override IEnumerator LoadingRoutine()
	{
		// 임의 페이크 로딩
		Debug.Log("랜덤 맵 생성");
		progress = 0.0f;
		//// 게임 시간은 멈춰있으니 실제 시간만큼 흘러가게 함
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("랜덤 몬스터 생성");
		progress = 0.2f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("랜덤 아이템 생성");
		progress = 0.4f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("플레이어 배치");
		progress = 0.6f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("로딩 완료");
		progress = 1.0f;
	}
}
