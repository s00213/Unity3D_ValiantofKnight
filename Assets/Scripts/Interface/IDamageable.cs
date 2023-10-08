using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
	// 공격당할 수 있는 모든 대상
	public void TakeDamage(int damage); 
}
