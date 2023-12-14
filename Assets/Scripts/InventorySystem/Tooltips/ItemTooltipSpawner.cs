using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Tooltips;

namespace UI.Inventories
{
	/// <summary>
	/// 아이템 설명 도구 생성기
	/// </summary>
	[RequireComponent(typeof(IItemHolder))]
    public class ItemTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            var item = GetComponent<IItemHolder>().GetItem();
            if (!item) return false;

            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            var itemTooltip = tooltip.GetComponent<ItemTooltip>();
            if (!itemTooltip) return;

            var item = GetComponent<IItemHolder>().GetItem();

            itemTooltip.Setup(item);
        }
    }
}