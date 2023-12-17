using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Achievement", fileName = "Achievement_")]
public class Achievement : Quest
{
	// 업적은 취소하지 못하게 함
	public override bool IsCancelable => false;
	
	// 업적은 저장됨
	public override bool IsSavable => true;

	public override void Cancel()
	{
		Debug.LogAssertion("Achievement can't be canceled");
	}
}
