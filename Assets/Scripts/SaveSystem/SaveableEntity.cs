using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Saving
{
	/// <summary>
	/// 저장 가능한 모든 데이터를 저장함
	/// </summary>
	[ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [Tooltip("모든 인스턴스를 원하는 경우가 아니면 프리팹에 설정하지 않")]
        [SerializeField] string uniqueIdentifier = "";

        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

		/// <summary>
		/// 이 구성 요소의 모든 ISaveables 상태를 캡처하고,
		/// 이 상태를 복원할 수 있는 `System.Serialized` 객체를 반환함
		/// </summary>
		public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

		/// <summary>
		/// CaptureState에 의해 캡처된 상태를 복원함
		/// </summary>
		/// <param name="state">
		/// CaptureState에서 반환된 동일한 개체
		/// </param>
		public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDict[typeString]);
                }
            }
        }

#if UNITY_EDITOR
        private void Update() {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            
            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
        }
#endif

        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            if (globalLookup[candidate] == this) return true;

            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }
    }
}