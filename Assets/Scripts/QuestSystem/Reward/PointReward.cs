using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "Quest/Reaward/Point", fileName = "PointReward_")]
public class PointReward : Reward
{
	private SpawnManager spawnManager;

	public override void Give(Quest quest)
	{
		spawnManager.DisplayScore(Quantity);
		PlayerPrefs.SetInt("bonusScore", Quantity);
		PlayerPrefs.Save();
	}
}
