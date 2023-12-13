using UnityEngine;

namespace Saving
{
    /// <summary>
    /// 플레이어 위치 저장
    /// </summary>
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z;

		/// <summary>
		/// 기존 Vector3에서 상태를 복사함
		/// </summary>
		public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

		/// <summary>
		/// 이 클래스의 상태에서 새로운 Vector3를 반환함
		/// </summary>
		/// <returns></returns>
		public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}