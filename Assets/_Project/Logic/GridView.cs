using System.Collections.Generic;
using System.Threading;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using static Cysharp.Threading.Tasks.UniTask;

namespace _Project
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private float _size = 1f;
        [SerializeField] private float _gap = .1f;
        [SerializeField] private NodeView _prefab;

        private Coroutine _highlightCoroutine;
        private readonly List<NodeView> _nodes = new();
        private Grid _grid;
        private Vector2Int? _clickTarget;
        private IPathSolver _pathSolver;

        [Inject]
        private void Construct(IPathSolver pathSolver) => 
            _pathSolver = pathSolver;

        public void Create(Grid grid)
        {
            _grid = grid;
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    NodeView nodeObject = Instantiate(_prefab, transform);

                    nodeObject.gameObject.name = $"Node_{x}_{y}";
                    SetupNode(nodeObject, x, y, grid.GetNode(x, y));

                    _nodes.Add(nodeObject);
                }
            }
        }

        private void SetupNode(NodeView nodeObject, int x, int y, Node model = null)
        {
            nodeObject.SetScale(new Vector3(_size, _size, _size));
            nodeObject.transform.position = transform.position
                                            + new Vector3(
                                                x * _size + x * _gap,
                                                0,
                                                y * _size + y * _gap);

            if (model != null)
                nodeObject.Model = model;

            nodeObject.Clicked += InvokeClick;
        }

        private void InvokeClick(NodeView nodeView)
        {
            _clickTarget = nodeView.Model.Position;
        }

        public Vector3 GetPosition(Vector2Int index) =>
            _nodes
                .Find(node => node.Model.Position == index)
                .transform.position
            + Vector3.up * .5f;

        public Vector3 GetPosition(Node node) =>
            GetPosition(node.Position);

        public async UniTask<Vector2Int?> WaitForClick(CancellationToken token)
        {
            while (!token.IsCancellationRequested
                   && (!_clickTarget.HasValue
                       || _grid.IsOccupied(_clickTarget!.Value)))
            {
                await WaitUntil(() => _clickTarget.HasValue, cancellationToken: token);
            }

            return token.IsCancellationRequested switch
            {
                true => null,
                _ => _clickTarget
            };
        }

        public void HighlightAvailableCells(Character source)
        {
            foreach (NodeView node in _nodes)
            {
                if (_pathSolver.CanReach(source.Node, node.Model, source.Stamina))
                    node.HighlightAsAvailable();
                else
                    node.Disable();
            }
        }
    }
}