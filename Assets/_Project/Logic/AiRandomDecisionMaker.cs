using System.Collections.Generic;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project
{
    public class AiRandomDecisionMaker : IDecisionMaker
    {
        private readonly AiAssistant _aiAssistant;

        public AiRandomDecisionMaker(AiAssistant aiAssistant) => 
            _aiAssistant = aiAssistant;

        public UniTask<IDecision> Execute(Character source, CharactersRepository enemies)
        {
            List<IDecision> decisions = _aiAssistant.GatherDecisions(source, enemies);
            IDecision decision = decisions[Random.Range(0, decisions.Count)];
            
            return UniTask.FromResult(decision);
        }
    }

    public class AiAssistant
    {
        private readonly Grid _grid;
        private readonly IPathSolver _pathSolver;
        private readonly ViewEventsQueue _viewEvents;

        public AiAssistant(Grid grid, IPathSolver pathSolver, ViewEventsQueue viewEvents)
        {
            _grid = grid;
            _pathSolver = pathSolver;
            _viewEvents = viewEvents;
        }

        public List<IDecision> GatherDecisions(Character source, CharactersRepository enemies)
        {
            List<IDecision> decisions = new();

            foreach (Node node in _grid.Nodes)
            {
                if (_pathSolver.CanReach(source.Node, node, source.Stamina, out Path path))
                    decisions.Add(new MoveDecision(source, path, _viewEvents));
            }

            foreach (Character enemy in enemies.All)
            {
                foreach (Node node in enemy.Node.Neighbours)
                {
                    if (!_pathSolver.CanReach(source.Node, node, source.Stamina, out Path path))
                        continue;
                    
                    if (path.Last.Occupant != source)
                        continue;
                    
                    if (source.Stamina < path.Stamina + source.AttackStamina)
                        continue;

                    Vector2Int direction = node.Position - enemy.Position;
                    
                }
            }
            
            return decisions;
        }
    }
}