using System.Linq;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace _Project.View
{
    public class CharacterView : MonoBehaviour
    {
        [field:SerializeField] public AttackSide[] AttackSides { get; private set; }
        public Character Character { get; private set; }
        
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private Transform _modelRoot;
        [SerializeField] private GameObject _outline;
        [SerializeField] private GameObject _targetOutline;
        [SerializeField] private GameObject _hitEffect;
        
        public void Setup(Character character, Color color, Vector3 position)
        {
            Character = character;
            _renderers.ForEach(x => x.material.color = color);
            transform.position = position;
        }

        public void ShowOutline() => 
            _outline.SetActive(true);
        
        public void HideOutline() => 
            _outline.SetActive(false);

        public void HideTargetOutline() => 
            _targetOutline.SetActive(false);

        public async UniTask Move(Vector3[] path)
        {
            Tween tween = transform
                .DOPath(path, 0.5f, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .OnWaypointChange(index =>
                {
                    if (index + 1 >= path.Length)
                        return;

                    Vector3 direction = path[index + 1] - path[index];
                    if (direction != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        _modelRoot.rotation = targetRotation;
                    }
                });

            await tween
                .AsyncWaitForCompletion()
                .AsUniTask();
        }

        public async UniTask ShowAttack()
        {
            _hitEffect.SetActive(true);
            await UniTask.Delay(500);
        }

        public async UniTask Die()
        {
            await transform
                .DOScale(Vector3.zero, .5f)
                .AsyncWaitForCompletion()
                .AsUniTask();
            
            Destroy(gameObject);
        }

        public async UniTask Rotate(CharacterView toTarget) =>
            await _modelRoot
                .DOLookAt(toTarget.transform.position, .5f)
                .AsyncWaitForCompletion()
                .AsUniTask();
    }
}