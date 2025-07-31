using System.Collections.Generic;
using _Pathfinding.Common;

namespace _Project
{
    public class AiDecisionsGatherService
    {
        private readonly Grid _grid;
        private readonly IPathSolver _pathSolver;
        private readonly ViewEventsQueue _viewEvents;

        public AiDecisionsGatherService(Grid grid, IPathSolver pathSolver, ViewEventsQueue viewEvents)
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

                    decisions.Add(new AttackDecision(source, enemy, path, _viewEvents, enemies));
                }
            }
            
            decisions.Add(new SkipDecision());
            
            return decisions;
        }
    }
}