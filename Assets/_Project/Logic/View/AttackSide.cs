using System;
using UnityEngine;

namespace _Project
{
    [Serializable]
    public class AttackSide
    {
        [field:SerializeField] public Vector2Int Direction { get; private set; }
        
        [SerializeField] private GameObject _target;

        public void Show() => 
            _target.SetActive(true);

        public void Hide() => 
            _target.SetActive(false);
    }
}