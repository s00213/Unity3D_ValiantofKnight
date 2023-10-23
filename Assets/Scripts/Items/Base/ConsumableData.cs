using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableData", menuName = "Item/ConsumableData")]
public class ConsumableData : Item
{
	public enum consumableType { Potion, Food }
	public consumableType typeOfConsumable;
 
    public int HPRecover;
}
