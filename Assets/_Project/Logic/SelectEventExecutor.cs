using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project
{
    public class SelectEventExecutor : IEventExecutor<SelectEvent>
    {
        private readonly CharactersViewMap _map;
        private readonly GridView _gridView;
        private readonly IPathSolver _pathSolver;

        public SelectEventExecutor(CharactersViewMap map, GridView gridView, IPathSolver pathSolver)
        {
            _map = map;
            _gridView = gridView;
            _pathSolver = pathSolver;
        }

        public UniTask Execute(SelectEvent @event)
        {
            _gridView.HighlightAvailableCells(@event.Character);
            
            _map
                .Get(@event.Character)
                .ShowOutline();

            _map
                .GetEnemies(@event.Character)
                .ForEach(x => ShowAsTarget(@event.Character, x.Character));
            
            return UniTask.CompletedTask;
        }

        private void ShowAsTarget(Character source, Character target)
        {
            foreach (Node node in target.Node.Neighbours)
            {
                bool freeAndCanReach = _pathSolver.CanReach(source.Node, node, source.Stamina, out Path path) && !node.IsOccupied;
                bool occupiedNotBySource = node.IsOccupied && node.Occupant != source;
                bool enoughStaminaToAttack = source.Stamina >= path.Stamina + source.AttackStamina;
                Vector2Int direction = node.Position - target.Position;

                if (freeAndCanReach && !occupiedNotBySource && enoughStaminaToAttack)
                    _map
                        .Get(target)
                        .ShowAttackSide(direction);
            }
        }
    }
}