namespace _Project
{
    public class AttackDecision : IDecision
    {
        public readonly Path Path; 
        public readonly Character Target;
        private readonly MoveDecision _moveDecision;
        private readonly Character _source;
        private readonly ViewEventsQueue _viewEvents;
        private readonly CharactersRepository _enemies;

        public AttackDecision(
            Character source, 
            Character target, 
            Path path, 
            ViewEventsQueue viewEvents,
            CharactersRepository enemies)
        {
            Path = path;
            _source = source;
            Target = target;
            _viewEvents = viewEvents;
            _enemies = enemies;
            
            if (!path.IsEmpty)
                _moveDecision = new MoveDecision(source, path, viewEvents);
        }

        public void Execute()
        {
            _moveDecision?.Execute();
            _source.Attack(Target);
            _viewEvents.Enqueue(new AttackEvent(_source, Target));
            
            if (Target.IsDead)
            {
                _viewEvents.Enqueue(new DieEvent(Target));
                _enemies.Remove(Target);
            }
        }
    }
}