using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy의 청각 능력
/// </summary>
public interface IListenable
{
	public void Listen(Transform trans);
}
