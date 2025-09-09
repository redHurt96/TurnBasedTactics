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
        private readonly AiDecisionsGatherService _decisionsGatherService;

        public AttackClickAwaiter(CharactersViewMap map, AiDecisionsGatherService decisionsGatherService)
        {
            _map = map;
            _decisionsGatherService = decisionsGatherService;
        }

        public async UniTask<(Character, Vector2Int)> WaitForClick(Character source,
            CharactersRepository enemiesRepository, CancellationToken token)
        {
            List<IDecision> decisions = _decisionsGatherService.GatherDecisions(source, enemiesRepository);
            List<(CharacterView, AttackSide)> attackSides = new();
            
            foreach (AttackDecision decision in decisions.OfType<AttackDecision>())
            {
                CharacterView view = _map.Get(decision.Target);
                Vector2Int direction = decision.Path.Last.Position - view.Character.Position;
                AttackSide attackSide = view.AttackSides.First(x => x.Direction == direction);

                attackSides.Add((view, attackSide));
            }
            
            if (!attackSides.Any())
                await UniTask.WaitUntilCanceled(token);
            
            if (token.IsCancellationRequested)
                return default;
            
            int index = await UniTask
                .WhenAny(attackSides
                    .Select(x => x.Item2.AwaitForClick(token)));

            return (attackSides[index].Item1.Character, attackSides[index].Item2.Direction);
        }
    }
}