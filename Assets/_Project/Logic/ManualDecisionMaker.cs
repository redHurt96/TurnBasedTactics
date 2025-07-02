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
        private readonly BreathFirstPathSolver _pathSolver;
        private readonly SkipButton _skipButton;
        private readonly AttackClickAwaiter _attackAwaiter;

        public ManualDecisionMaker(
            GridView gridView, 
            ViewEventsQueue viewEvents, 
            BreathFirstPathSolver pathSolver, 
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

            return index switch
            {
                0 => new MoveDecision(source, cell.Value, _pathSolver, _viewEvents),
                1 => new SkipDecision(),
                2 => new AttackDecision(source, target.Item1, target.Item2, _pathSolver, _viewEvents, enemies),
                _ => new ErrorDecision()
            };
        }
    }
}