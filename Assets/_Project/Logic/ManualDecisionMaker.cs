using System.Threading;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project
{
    public class ManualDecisionMaker : IDecisionMaker
    {
        private readonly GridView _gridView;
        private readonly ViewEventsQueue _viewEvents;
        private readonly IPathSolver _pathSolver;
        private readonly SkipButton _skipButton;
        private readonly AttackClickAwaiter _attackAwaiter;

        public ManualDecisionMaker(
            GridView gridView, 
            ViewEventsQueue viewEvents, 
            IPathSolver pathSolver, 
            SkipButton skipButton,
            AttackClickAwaiter attackAwaiter)
        {
            _gridView = gridView;
            _viewEvents = viewEvents;
            _pathSolver = pathSolver;
            _skipButton = skipButton;
            _attackAwaiter = attackAwaiter;
        }

        public async UniTask<IDecision> Execute(Character source, CharactersRepository enemies)
        {
            CancellationTokenSource tokenSource = new();

            (int index, Vector2Int? cell, bool _, (Character, Vector2Int) target) = 
                await UniTask.WhenAny(
                _gridView.WaitForClick(tokenSource.Token),
                _skipButton.WaitForClick(tokenSource.Token),
                _attackAwaiter.WaitForClick(source, tokenSource.Token));
            
            tokenSource.Cancel();

            switch (index)
            {
                case 0:
                    Path path = _pathSolver.Find(source.Position, cell.Value);
                    return new MoveDecision(source, path, _viewEvents);
                case 1:
                    return new SkipDecision();
                case 2:
                    path = _pathSolver.Find(source.Position, target.Item1.Position + target.Item2);
                    return new AttackDecision(source, target.Item1, path, _viewEvents, enemies);
                default:
                    return new ErrorDecision();
            }
        }
    }
}