using UnityEngine;

namespace Inventories
{
	/// <summary>
	/// 플레이어가 장착할 수 있는 인벤토리 아이템
	/// </summary>
	[CreateAssetMenu(menuName = ("UI.InventorySystem/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        [Tooltip("장비 놓는 위치")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    }
}