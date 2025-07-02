using System.Linq;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project
{
    public class MoveEventSubscriber : IEventSubscriber<MoveEvent>
    {
        private readonly GridView _gridView;
        private readonly CharactersViewMap _map;

        public MoveEventSubscriber(GridView gridView, CharactersViewMap map)
        {
            _gridView = gridView;
            _map = map;
        }

        public async UniTask Execute(MoveEvent @event)
        {
            Vector3 target = _gridView.GetPosition(@event.Path.Last());
            await _map
                .Get(@event.Character)
                .Move(target);
        }
    }
}