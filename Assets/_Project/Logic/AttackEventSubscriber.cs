using Cysharp.Threading.Tasks;

namespace _Project
{
    public class AttackEventSubscriber : IEventSubscriber<AttackEvent>
    {
        private readonly CharactersViewMap _map;

        public AttackEventSubscriber(CharactersViewMap map) => 
            _map = map;

        public async UniTask Execute(AttackEvent @event) =>
            await _map
                .Get(@event.Target)
                .ShowAttack();
    }
}