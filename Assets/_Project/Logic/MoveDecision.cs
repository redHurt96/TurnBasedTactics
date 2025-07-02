using System.Collections.Generic;
using System.Linq;
using _Pathfinding.Common;
using UnityEngine;

namespace _Project
{
    public class MoveDecision : IDecision
    {
        private readonly Character _source;
        private readonly Vector2Int _target;
        private readonly BreathFirstPathSolver _pathSolver;
        private readonly ViewEventsQueue _viewEvents;

        public MoveDecision(Character source, Vector2Int target, BreathFirstPathSolver pathSolver, ViewEventsQueue viewEvents)
        {
            _source = source;
            _target = target;
            _pathSolver = pathSolver;
            _viewEvents = viewEvents;
        }

        public void Execute()
        {
            List<Node> path = _pathSolver.Find(_source.Position, _target);
            _source.Move(path.Last());

            _viewEvents.Enqueue(new MoveEvent
            {
                Character = _source,
                Path = path,
            });
        }
    }
}