using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;

namespace _Project
{
    public class SelectEventSubscriber : IEventSubscriber<SelectEvent>
    {
        private readonly CharactersViewMap _map;

        public SelectEventSubscriber(CharactersViewMap map) => 
            _map = map;

        public UniTask Execute(SelectEvent @event)
        {
            _map
                .Get(@event.Character)
                .ShowOutline();

            _map
                .GetEnemies(@event.Character)
                .ForEach(x => x.ShowAsTarget(@event.Character));
            
            return UniTask.CompletedTask;
        }
    }
}