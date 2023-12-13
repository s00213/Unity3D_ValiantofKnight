using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dragging
{
	/// <summary>
	/// 두 컨테이너 사이에서 항목을 교환함
	/// </summary>
	/// <typeparam name="T"> 드래그 되는 항목을 나타내는 유형임 </typeparam>
	public interface IDragContainer<T> : IDragDestination<T>, IDragSource<T> where T : class
    {
    }
}