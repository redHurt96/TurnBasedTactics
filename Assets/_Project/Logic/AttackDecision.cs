using _Pathfinding.Common;
using UnityEngine;

namespace _Project
{
    public class AttackDecision : IDecision
    {
        private readonly MoveDecision _moveDecision;
        private readonly Character _source;
        private readonly Character _target;
        private readonly ViewEventsQueue _viewEvents;
        private readonly CharactersRepository _enemies;

        public AttackDecision(
            Character source, 
            Character target, 
            Vector2Int direction, 
            BreathFirstPathSolver pathSolver, 
            ViewEventsQueue viewEvents,
            CharactersRepository enemies)
        {
            _source = source;
            _target = target;
            _viewEvents = viewEvents;
            _enemies = enemies;
            _moveDecision = new MoveDecision(source, target.Position + direction, pathSolver, viewEvents);
        }

        public void Execute()
        {
            _moveDecision.Execute();
            _target.TakeDamage(_source.Damage);
            _viewEvents.Enqueue(new AttackEvent(_target));
            
            if (_target.IsDead)
            {
                _viewEvents.Enqueue(new DieEvent(_target));
                _enemies.Remove(_target);
            }
        }
    }
}