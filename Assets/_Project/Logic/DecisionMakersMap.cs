using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(menuName = "Create DecisionMakersMap", fileName = "DecisionMakersMap", order = 0)]
    public class DecisionMakersMap : SerializedScriptableObject
    {
        public Dictionary<int, Type> DecisionMakersPerType = new();
    }
}