using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dragging
{
	/// <summary>
	/// DragItem을 드래그함
	/// </summary>
	/// <typeparam name="T"> 드래그되는 항목을 나타냄 </typeparam>
	public interface IDragSource<T> where T : class
    {
        /// <summary>
        /// 어떤 아이템인지
        /// </summary>
        T GetItem();

        /// <summary>
        /// 수량이 몇 개인지
        /// </summary>
        int GetNumber();

		/// <summary>
		/// 지정된 수의 항목을 제거
		/// </summary>
		/// <param name="number">
		/// GetNumber가 반환한 숫자를 초과해서는 안 됨
		/// </param>
		void RemoveItems(int number);
    }
}