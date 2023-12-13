namespace Saving
{
	/// <summary>
	/// 저장 또는 복원할 수 있음
	/// </summary>
	public interface ISaveable
    {
		/// <summary>
		/// 구성요소의 상태를 캡처하기 위해 저장할 때 호출됨
		/// </summary>
		/// <returns>
		/// 상태를 나타내는 System.Serialized 객체 요소를 반환함
		/// </returns>
		object CaptureState();

		/// <summary>
		/// 장면의 상태를 복원할 때 호출됨
		/// </summary>
		/// <param name="state">
		/// 저장할 때 CaptureState에서 반환된 것과 동일한 System.Serializable 객체임
		/// </param>
		void RestoreState(object state);
    }
}