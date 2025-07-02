using _Project.View;
using Cysharp.Threading.Tasks;

namespace _Project
{
    public class DieEventSubscriber : IEventSubscriber<DieEvent>
    {
        private readonly CharactersViewMap _map;

        public DieEventSubscriber(CharactersViewMap map) => 
            _map = map;

        public async UniTask Execute(DieEvent @event)
        {
            CharacterView view = _map.Get(@event.Target);

            await view.Die();

            _map.Remove(@event.Target);
        }
    }
}