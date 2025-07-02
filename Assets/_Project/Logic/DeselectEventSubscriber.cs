using Cysharp.Threading.Tasks;

namespace _Project
{
    public class DeselectEventSubscriber : IEventSubscriber<DeselectEvent>
    {
        private readonly CharactersViewMap _map;

        public DeselectEventSubscriber(CharactersViewMap map) => 
            _map = map;

        public UniTask Execute(DeselectEvent @event)
        {
            _map
                .Get(@event.Character)
                .HideOutline();

            _map
                .ForEachView(x => x.HideTargetOutline());
            
            return UniTask.CompletedTask;
        }
    }
}