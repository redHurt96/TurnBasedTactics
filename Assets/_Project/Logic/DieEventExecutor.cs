using _Project.View;
using Cysharp.Threading.Tasks;

namespace _Project
{
    public class DieEventExecutor : IEventExecutor<DieEvent>
    {
        private readonly CharactersViewMap _map;

        public DieEventExecutor(CharactersViewMap map) => 
            _map = map;

        public async UniTask Execute(DieEvent @event)
        {
            CharacterView view = _map.Get(@event.Target);

            await view.Die();

            _map.Remove(@event.Target);
        }
    }
}