using Cysharp.Threading.Tasks;

namespace _Project
{
    public class AttackEventSubscriber : IEventSubscriber<AttackEvent>
    {
        private readonly CharactersViewMap _map;

        public AttackEventSubscriber(CharactersViewMap map) => 
            _map = map;

        public UniTask Execute(AttackEvent @event)
        {
            _map
                .Get(@event.Target)
                .ShowAttack();
            
            return UniTask.CompletedTask;
        }
    }
}