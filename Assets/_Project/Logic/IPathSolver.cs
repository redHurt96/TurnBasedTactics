using _Project;
using UnityEngine;

namespace _Pathfinding.Common
{
    public interface IPathSolver
    {
        Path Find(Vector2Int from, Vector2Int to);
        bool CanReach(Node from, Node to, int withStamina, out Path path);
        bool CanReach(Node from, Node to, int withStamina);
    }
}