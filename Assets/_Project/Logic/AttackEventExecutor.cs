using _Project.View;
using Cysharp.Threading.Tasks;

namespace _Project
{
    public class AttackEventExecutor : IEventExecutor<AttackEvent>
    {
        private readonly CharactersViewMap _map;

        public AttackEventExecutor(CharactersViewMap map) => 
            _map = map;

        public async UniTask Execute(AttackEvent @event)
        {
            CharacterView target = _map.Get(@event.Target);
            
            _map
                .GetUi(@event.Source)
                .UpdateStamina();
            
            await _map
                .Get(@event.Source)
                .Rotate(target);

            await target
                .ShowAttack();
            
            _map
                .GetUi(@event.Target)
                .UpdateHealth();
        }
    }
}