using _Project.View;
using Cysharp.Threading.Tasks;

namespace _Project
{
    public class RestoreStaminaEventExecutor : IEventExecutor<RestoreStaminaEvent>
    {
        private readonly CharactersViewMap _map;

        public RestoreStaminaEventExecutor(CharactersViewMap map)
        {
            _map = map;
        }

        public UniTask Execute(RestoreStaminaEvent @event)
        {
            CharacterUiView view = _map.GetUi(@event.Character);
            view.UpdateStamina();
            
            return UniTask.CompletedTask;
        }
    }
}