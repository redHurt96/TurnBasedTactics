namespace _Project
{
    public class MoveDecision : IDecision
    {
        private readonly Character _source;
        private readonly Path _path;
        private readonly ViewEventsQueue _viewEvents;

        public MoveDecision(Character source, Path path, ViewEventsQueue viewEvents)
        {
            _source = source;
            _path = path;
            _viewEvents = viewEvents;
        }

        public void Execute()
        {
            _source.Move(_path);
            _viewEvents.Enqueue(new MoveEvent
            {
                Character = _source,
                Path = _path,
            });
        }
    }
}