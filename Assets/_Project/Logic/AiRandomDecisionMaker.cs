using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace _Project
{
    public class AiRandomDecisionMaker : IDecisionMaker
    {
        private readonly AiDecisionsGatherService _aiDecisionsGatherService;

        public AiRandomDecisionMaker(AiDecisionsGatherService aiDecisionsGatherService) => 
            _aiDecisionsGatherService = aiDecisionsGatherService;

        public async UniTask<IDecision> Execute(Character source, CharactersRepository enemies)
        {
            List<IDecision> decisions = _aiDecisionsGatherService.GatherDecisions(source, enemies);
            IDecision decision = decisions[Random.Range(0, decisions.Count)];

            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            
            return decision;
        }
    }
}