using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy HPBar가 카메라 쪽을 볼 수 있도록 함
public class Billboard : MonoBehaviour
{
	private void LateUpdate()
	{
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
	}
}
