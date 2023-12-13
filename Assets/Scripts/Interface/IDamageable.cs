using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
	/// <summary>
	/// 공격당할 수 있는 모든 대상
	/// </summary>
	/// <param name="damage"> 데미지 </param>
	public void TakeDamage(int damage); 
}
