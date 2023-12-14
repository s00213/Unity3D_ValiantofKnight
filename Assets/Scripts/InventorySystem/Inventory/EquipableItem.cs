using UnityEngine;

namespace Inventories
{
	/// <summary>
	/// 플레이어가 장착할 수 있는 인벤토리 아이템
	/// </summary>
	[CreateAssetMenu(menuName = ("InventorySystem/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        [Tooltip("놓을 수 있는 위치가 설정되어 있음")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    }
}