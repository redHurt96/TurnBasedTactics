using System.Collections.Generic;
using System.Threading;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project
{
    public class ManualDecisionMaker : IDecisionMaker
    {
        private readonly GridView _gridView;
        private readonly MessagesQueue _messages;
        private readonly BreathFirstPathSolver _pathSolver;

        public ManualDecisionMaker(GridView gridView, MessagesQueue messages, BreathFirstPathSolver pathSolver)
        {
            _gridView = gridView;
            _messages = messages;
            _pathSolver = pathSolver;
        }

        public async UniTask<IDecision> Execute(Character source, CharactersRepository enemies)
        {
            CancellationTokenSource tokenSource = new();
            UniTask<Vector2Int?> click = _gridView.WaitForClick(tokenSource.Token);

            Vector2Int? result = await click;
            
            if (result.HasValue)
                return CreateMoveDecision(source, result.Value);
            
            return new ErrorDecision();
        }

        private IDecision CreateMoveDecision(Character source, Vector2Int result)
        {
            List<Node> path = _pathSolver.Find(source.Position, result);
            return new MoveDecision(source, path, _messages);
        }
    }
}