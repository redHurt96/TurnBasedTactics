using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project
{
    public class AttackClickAwaiter
    {
        private readonly CharactersViewMap _map;

        public AttackClickAwaiter(CharactersViewMap map) => 
            _map = map;

        public async UniTask<(Character, Vector2Int)> WaitForClick(Character source, CancellationToken token)
        {
            IEnumerable<CharacterView> characters = _map.GetEnemies(source);
            (CharacterView, AttackSide)[] attackSides = characters
                .SelectMany(x => x.AttackSides.Select(y => (x, y)))
                .ToArray();

            int index = await UniTask
                .WhenAny(attackSides
                    .Select(x => x.Item2.AwaitForClick(token)));
            
            return (attackSides[index].Item1.Character, attackSides[index].Item2.Direction);
        }
    }
}