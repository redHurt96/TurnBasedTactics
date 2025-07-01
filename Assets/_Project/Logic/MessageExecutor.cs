using System.Linq;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project
{
    public class MessageExecutor
    {
        private readonly CharactersViewMap _charactersViewMap;
        private readonly GridView _gridView;

        public MessageExecutor(CharactersViewMap charactersViewMap, GridView gridView)
        {
            _charactersViewMap = charactersViewMap;
            _gridView = gridView;
        }

        public void Execute(SelectEvent @event) =>
            _charactersViewMap
                .Get(@event.Character)
                .ShowOutline();

        public void Execute(DeselectEvent @event)
        {
            _charactersViewMap
                .Get(@event.Character)
                .HideOutline();

            _charactersViewMap
                .ForEachView(x => x.HideTargetOutline());
        }

        public async UniTask Execute(MoveEvent @event)
        {
            Vector3 target = _gridView.GetPosition(@event.Path.Last());
            await _charactersViewMap
                .Get(@event.Character)
                .Move(target);
        }
    }
}