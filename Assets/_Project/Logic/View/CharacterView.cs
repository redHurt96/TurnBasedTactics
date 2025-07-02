using System.Linq;
using System.Threading.Tasks;
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
        
        [SerializeField] private Renderer _renderer;
        [SerializeField] private GameObject _outline;
        [SerializeField] private GameObject _targetOutline;
        [SerializeField] private GameObject _hitEffect;
        
        private BreathFirstPathSolver _pathSolver;

        [Inject]
        private void Construct(BreathFirstPathSolver pathSolver) => 
            _pathSolver = pathSolver;

        public void Setup(Character character, Color color, Vector3 position)
        {
            Character = character;
            _renderer.material.color = color;
            transform.position = position;
        }

        public void ShowOutline() => 
            _outline.SetActive(true);
        
        public void ShowAsTarget(Character forEnemy)
        {
            _targetOutline.SetActive(true);
            
            foreach (Node node in Character.Node.Neighbours)
            {
                bool freeAndCanReach = !node.IsOccupied && CanReachNode(forEnemy, node);
                bool occupiedByTarget = node.IsOccupied && node.Occupant == forEnemy;
                
                if (freeAndCanReach || occupiedByTarget)
                {
                    Vector2Int direction = node.Position - Character.Position;
                    ShowAttackSide(direction);
                }
            }
        }

        private bool CanReachNode(Character forEnemy, Node node) => 
            _pathSolver.CanReach(forEnemy.Node, node);

        public void HideOutline() => 
            _outline.SetActive(false);

        public void HideTargetOutline()
        {
            _targetOutline.SetActive(false);
            AttackSides.ForEach(x => x.Hide());
        }

        public async UniTask Move(Vector3[] path) =>
            await transform
                .DOPath(path, .5f, PathType.CatmullRom)
                .SetEase(Ease.Linear) 
                .AsyncWaitForCompletion()
                .AsUniTask();

        public async UniTask ShowAttack()
        {
            _hitEffect.SetActive(true);
            await UniTask.Delay(500);
        }

        private void ShowAttackSide(Vector2Int direction) =>
            AttackSides
                .First(x => x.Direction == direction)
                .Show();

        public async UniTask Die()
        {
            await transform
                .DOScale(Vector3.zero, .5f)
                .AsyncWaitForCompletion()
                .AsUniTask();
            
            Destroy(gameObject);
        }
    }
}