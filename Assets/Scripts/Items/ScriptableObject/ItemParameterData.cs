using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu]
    public class ItemParameterData : ScriptableObject
    {
        [field: SerializeField]
        public string ParameterName { get; private set; }
    }
}