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
        private readonly SkipButton _skipButton;

        public ManualDecisionMaker(GridView gridView, MessagesQueue messages, BreathFirstPathSolver pathSolver, SkipButton skipButton)
        {
            _gridView = gridView;
            _messages = messages;
            _pathSolver = pathSolver;
            _skipButton = skipButton;
        }

        public async UniTask<IDecision> Execute(Character source, CharactersRepository enemies)
        {
            CancellationTokenSource tokenSource = new();

            (int index, Vector2Int? cell, bool _) = await UniTask.WhenAny(
                _gridView.WaitForClick(tokenSource.Token),
                _skipButton.WaitForClick(tokenSource.Token));
            
            tokenSource.Cancel();
            
            if (index == 0)
                return CreateMoveDecision(source, cell.Value);

            if (index == 1)
                return CreateSkipDecision(source);
            
            return new ErrorDecision();
        }

        private IDecision CreateMoveDecision(Character source, Vector2Int result)
        {
            List<Node> path = _pathSolver.Find(source.Position, result);
            return new MoveDecision(source, path, _messages);
        }

        private IDecision CreateSkipDecision(Character source) => 
            new SkipDecision(source);
    }
}