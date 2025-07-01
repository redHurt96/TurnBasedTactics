using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;
using Grid = _Project.Grid;

namespace _Pathfinding.Common
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private float _size = 1f;
        [SerializeField] private float _gap = .1f;
        [SerializeField] private NodeView _prefab;

        private Coroutine _highlightCoroutine;
        private readonly Queue<Node> _visitedNodes = new();
        private readonly List<NodeView> _nodes = new();
        private Grid _grid;
        private Vector2Int? _clickTarget;

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

        public void Highlight(List<Node> path)
        {
            while (_visitedNodes.Count > 0)
            {
                Node node = _visitedNodes.Dequeue();
                NodeView nodeView = _nodes.FirstOrDefault(n => n.Model == node);

                nodeView!.HighlightAsVisited();

                //yield return new WaitForSeconds(_highlightDelay);
            }

            foreach (Node node in path)
            {
                NodeView nodeView = _nodes.FirstOrDefault(n => n.Model == node);

                nodeView!.HighlightAsPath();
            }

            _highlightCoroutine = null;
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

        [Button]
        private void DropHighLights()
        {
            foreach (NodeView node in _nodes)
            {
                node.DropHighlight();
                node.EnableMouse();
            }
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
    }
}