using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

	public ConsumableData consumableData;

	public int HPpotionCount;

	private void Awake()
	{
		instance = this;
		HPpotionCount = 0;	
	}
}
