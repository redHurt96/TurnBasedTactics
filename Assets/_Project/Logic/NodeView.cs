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

        [SerializeField] private float _scaleDuration = .5f;
        [SerializeField] private Ease _scaleEasing;
        
        [Space]
        [SerializeField] private float _moveDuration = .8f;
        [SerializeField] private float _shiftYCoefficient = .2f;
        
        [Space]
        [SerializeField] private float _hoverDuration = .2f;
        [SerializeField] private float _hoverScaleCoefficient = 1.2f;
        [SerializeField] private Ease _hoverScaleEasing = Ease.OutQuad;
        [SerializeField] private Ease _hoverColorEasing = Ease.OutQuad;
        [SerializeField] private Color _hoverColor = Color.white;
        
        [Space]
        [SerializeField] private float _visitedDuration = .2f;
        [SerializeField] private float _visitedScaleCoefficient = 1.2f;
        [SerializeField] private float _visitedYShift = .1f;
        [SerializeField] private Ease _visitedYShiftEasing = Ease.OutQuad;
        [SerializeField] private Ease _visitedScaleEasing = Ease.OutQuad;
        [SerializeField] private Ease _visitedColorEasing = Ease.OutQuad;
        [SerializeField] private Color _visitedColor = Color.white;
        
        [Space]
        [SerializeField] private float _pathDuration = .2f;
        [SerializeField] private float _pathScaleCoefficient = 1.2f;
        [SerializeField] private Ease _pathScaleEasing = Ease.OutQuad;
        [SerializeField] private Ease _pathColorEasing = Ease.OutQuad;
        [SerializeField] private Color _pathColor = Color.white;

        [Space] 
        [SerializeField, ReadOnly] private bool _interactWithMouse = true;
        
        private Color _originColor;
        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _originColor = _renderer.material.color;
        }

        public void SetScale(Vector3 scale)
        {
            Scale = scale;
            transform.localScale = scale;
        }

        public void RunCreateAnimation()
        {
            transform.localScale = Vector3.zero;
            transform
                .DOScale(Scale, _scaleDuration)
                .SetEase(_scaleEasing);

            Vector3 targetPosition = transform.position;
            transform.position += Vector3.down * (Scale.x * _shiftYCoefficient);
            transform
                .DOMove(targetPosition, _moveDuration)
                .SetEase(Ease.OutBack);
        }

        // private void OnMouseEnter()
        // {
        //     if (!_interactWithMouse)
        //         return;
        //     
        //     transform
        //         .DOScale(Scale * _hoverScaleCoefficient, _hoverDuration)
        //         .SetEase(_hoverScaleEasing);
        //     _renderer.material
        //         .DOColor(_hoverColor, _hoverDuration)
        //         .SetEase(_hoverColorEasing);
        // }
        //
        // private void OnMouseExit()
        // {
        //     if (!_interactWithMouse)
        //         return;
        //     
        //     DropHighlight();
        // }
        //
        private void OnMouseDown()
        {
            if (!_interactWithMouse)
                return;
            
            if (Input.GetMouseButtonDown(0))
                Clicked?.Invoke(this);
        }

        public void HighlightAsVisited()
        {
            _interactWithMouse = false;
            transform
                .DOScale(Scale * _visitedScaleCoefficient, _visitedDuration)
                .SetEase(_visitedScaleEasing);
            transform
                .DOMoveY(transform.position.y + _visitedYShift, _visitedDuration)
                .SetEase(_visitedYShiftEasing)
                .SetLoops(2, LoopType.Yoyo);
            _renderer.material
                .DOColor(_visitedColor, _visitedDuration)
                .SetEase(_visitedColorEasing);
        }

        public void HighlightAsPath()
        {
            _interactWithMouse = false;
            transform
                .DOScale(Scale * _pathScaleCoefficient, _pathDuration)
                .SetEase(_pathScaleEasing);
            _renderer.material
                .DOColor(_pathColor, _pathDuration)
                .SetEase(_pathColorEasing);
        }

        public void DropHighlight()
        {
            transform
                .DOScale(Scale, _hoverDuration)
                .SetEase(_hoverScaleEasing);
            _renderer.material
                .DOColor(_originColor, _hoverDuration)
                .SetEase(_hoverColorEasing);
        }

        public void DisableMouse()
        {
            _interactWithMouse = false;
        }

        public void EnableMouse()
        {
            _interactWithMouse = true;
        }
    }
}