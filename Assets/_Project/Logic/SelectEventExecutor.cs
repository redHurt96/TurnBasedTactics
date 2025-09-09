using Cysharp.Threading.Tasks;

namespace _Project
{
    public class SelectEventExecutor : IEventExecutor<SelectEvent>
    {
        private readonly CharactersViewMap _map;
        private readonly GridView _gridView;

        public SelectEventExecutor(CharactersViewMap map, GridView gridView)
        {
            _map = map;
            _gridView = gridView;
        }

        public UniTask Execute(SelectEvent @event)
        {
            _gridView.HighlightAvailableCells(@event.Character);
            
            _map
                .Get(@event.Character)
                .ShowOutline();

            return UniTask.CompletedTask;
        }
    }
}