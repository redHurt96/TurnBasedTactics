using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Pathfinding.Common
{
    internal class Flag : MonoBehaviour
    {
        [SerializeField] private Transform _stand;
        [SerializeField] private Transform _pole;
        [SerializeField] private Transform _flag;

        [Space]
        [SerializeField] private float _standScaleDuration = .5f;
        [SerializeField] private Ease _standScaleEase = Ease.OutBack;
        
        [Space]
        [SerializeField] private float _poleOriginShiftY = -1f;
        [SerializeField] private float _poleTargetShiftY = 4f;
        [SerializeField] private float _poleMoveOriginTime = .3f;
        [SerializeField] private float _poleMoveDuration = .5f;
        [SerializeField] private Ease _poleMoveEase = Ease.OutBack;
        
        [Space]
        [SerializeField] private float _flagOriginShiftY = -1f;
        [SerializeField] private float _flagTargetShiftY = 4f;
        [SerializeField] private float _flagMoveOriginTime = .7f;
        [SerializeField] private float _flagMoveDuration = .5f;
        [SerializeField] private float _flagRotateOriginTime = .5f;
        [SerializeField] private float _flagRotateDuration = .5f;
        [SerializeField] private Vector3 _flagRotateAngle;
        [SerializeField] private Ease _flagMoveEase = Ease.OutBack;
        [SerializeField] private Ease _flagRotateEase = Ease.OutBack;
        [SerializeField] private LoopType _flagRotateLoopType = LoopType.Incremental;
        
        public void RunSpawnAnimation()
        {
            _stand.localScale = Vector3.zero;
            _pole.transform.position += Vector3.down * _poleOriginShiftY;
            _flag.transform.position += Vector3.down * _flagOriginShiftY;
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(
                _stand
                    .DOScale(Vector3.one * 100, _standScaleDuration)
                    .SetEase(_standScaleEase));
            
            sequence.Insert(_poleMoveOriginTime,
                _pole
                    .DOMoveY(_pole.transform.position.y + _poleTargetShiftY, _poleMoveDuration)
                    .SetEase(_poleMoveEase));
            
            sequence.Insert(_flagMoveOriginTime,
                _flag
                    .DOMoveY(_flag.transform.position.y + _flagTargetShiftY, _flagMoveDuration)
                    .SetEase(_flagMoveEase));
            
            sequence.Insert(_flagRotateOriginTime,
                _flag
                    .DORotate(_flagRotateAngle, _flagRotateDuration)
                    .SetEase(_flagRotateEase)
                    .SetLoops(2, _flagRotateLoopType));
        }

        public void RunMoveAnimation() =>
            _flag
                .DORotate(_flagRotateAngle, _flagRotateDuration)
                .SetEase(_flagRotateEase)
                .SetLoops(2, _flagRotateLoopType);
    }
}