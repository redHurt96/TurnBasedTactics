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
        private readonly IPathSolver _pathSolver;
        private readonly ViewEventsQueue _viewEvents;

        public MoveDecision(Character source, Vector2Int target, IPathSolver pathSolver, ViewEventsQueue viewEvents)
        {
            _source = source;
            _target = target;
            _pathSolver = pathSolver;
            _viewEvents = viewEvents;
        }

        public void Execute()
        {
            if (_source.Position == _target)
                return;
            
            Path path = _pathSolver.Find(_source.Position, _target);
            _source.Move(path);

            _viewEvents.Enqueue(new MoveEvent
            {
                Character = _source,
                Path = path,
            });
        }
    }
}