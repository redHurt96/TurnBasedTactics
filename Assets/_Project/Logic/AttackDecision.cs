using _Pathfinding.Common;
using UnityEngine;

namespace _Project
{
    public class AttackDecision : IDecision
    {
        private readonly MoveDecision _moveDecision;
        private readonly Character _source;
        private readonly Character _target;
        private readonly Path _path;
        private readonly ViewEventsQueue _viewEvents;
        private readonly CharactersRepository _enemies;

        public AttackDecision(
            Character source, 
            Character target, 
            Path path, 
            ViewEventsQueue viewEvents,
            CharactersRepository enemies)
        {
            _source = source;
            _target = target;
            _path = path;
            _viewEvents = viewEvents;
            _enemies = enemies;
            _moveDecision = new MoveDecision(source, path, viewEvents);
        }

        public void Execute()
        {
            _moveDecision.Execute();
            _source.Attack(_target);
            _viewEvents.Enqueue(new AttackEvent(_source, _target));
            
            if (_target.IsDead)
            {
                _viewEvents.Enqueue(new DieEvent(_target));
                _enemies.Remove(_target);
            }
        }
    }
}