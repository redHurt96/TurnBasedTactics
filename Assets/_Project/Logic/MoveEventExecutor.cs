using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project
{
    public class MoveEventExecutor : IEventExecutor<MoveEvent>
    {
        private readonly GridView _gridView;
        private readonly CharactersViewMap _map;

        public MoveEventExecutor(GridView gridView, CharactersViewMap map)
        {
            _gridView = gridView;
            _map = map;
        }

        public async UniTask Execute(MoveEvent @event)
        {
            Vector3[] path = @event.Path
                .Select(x => _gridView.GetPosition(x))
                .ToArray();
            
            _map
                .GetUi(@event.Character)
                .UpdateStamina();
            
            await _map
                .Get(@event.Character)
                .Move(path);
        }
    }
}