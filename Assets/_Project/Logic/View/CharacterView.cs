using System.Linq;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace _Project
{
    public class CharacterView : MonoBehaviour
    {
        private Character _character;
        
        [SerializeField] private Renderer _renderer;
        [SerializeField] private GameObject _outline;
        [SerializeField] private GameObject _targetOutline;
        [SerializeField] private AttackSide[] _attackSides;
        
        private CharactersViewMap _charactersViewMap;
        private BreathFirstPathSolver _pathSolver;

        [Inject]
        private void Construct(CharactersViewMap charactersViewMap, BreathFirstPathSolver pathSolver)
        {
            _pathSolver = pathSolver;
            _charactersViewMap = charactersViewMap;
        }

        public void Setup(Character character, Color color, Vector3 position)
        {
            _character = character;
            _renderer.material.color = color;
            transform.position = position;
        }

        public void ShowOutline() => 
            _outline.SetActive(true);
        
        public void ShowAsTarget(Character forEnemy)
        {
            _targetOutline.SetActive(true);
            
            foreach (Node node in _character.Node.Neighbours)
            {
                bool freeAndCanReach = !node.IsOccupied && CanReachNode(forEnemy, node);
                bool occupiedByTarget = node.IsOccupied && node.Occupant == forEnemy;
                
                if (freeAndCanReach || occupiedByTarget)
                {
                    Vector2Int direction = node.Position - _character.Position;
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
            _attackSides.ForEach(x => x.Hide());
        }

        public async UniTask Move(Vector3 to) => 
            await transform
                .DOMove(to, .5f)
                .AsyncWaitForCompletion()
                .AsUniTask();

        private void ShowAttackSide(Vector2Int direction) =>
            _attackSides
                .First(x => x.Direction == direction)
                .Show();
    }
}