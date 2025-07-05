using System;
using _Project;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Pathfinding.Common
{
    public class NodeView : MonoBehaviour
    {
        public event Action<NodeView> Clicked;
        
        public Vector3 Scale { get; private set; }
        public Node Model { get; set; }

        [SerializeField] private Color _availableColor;
        [SerializeField] private Color _defaultColor;
        
        private Renderer _renderer;
        private bool _canInteract;

        private void Awake() => 
            _renderer = GetComponent<Renderer>();

        public void SetScale(Vector3 scale)
        {
            Scale = scale;
            transform.localScale = scale;
        }
        
        private void OnMouseDown()
        {
            if (!_canInteract) 
                return;
            
            if (Input.GetMouseButtonDown(0))
                Clicked?.Invoke(this);
        }
        
        public void HighlightAsAvailable()
        {
            _canInteract = true;
            _renderer.material.color = _availableColor;
        }

        public void Disable()
        {
            _canInteract = false;
            _renderer.material.color = _defaultColor;
        }
    }
}