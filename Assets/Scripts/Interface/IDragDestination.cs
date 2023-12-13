using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dragging
{
    /// <summary>
    /// DragItem을 드래그함
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDragDestination<T> where T : class
    {
		/// <summary>
		/// 최대 수량
		/// </summary>
		/// <param name="item"> 추가될 아이템 </param>
		/// <returns> 제한이 없으면 Int.MaxValue가 반환되어야 함 </returns>
		int MaxAcceptable(T item);

		/// <summary>
		/// 이 대상에 항목 추가를 반영하도록 UI와 모든 데이터를 업데이트함
		/// </summary>
		/// <param name="item"> 아이템 유형 </param>
		/// <param name="number"> 아이템의 수량 </param>
		void AddItems(T item, int number);
    }
}