using System.Collections.Generic;
using _Pathfinding.Common;
using UnityEngine;

namespace _Project
{
    public class BreathFirstPathSolver : IPathSolver
    {
        private readonly Grid _grid;

        public BreathFirstPathSolver(Grid grid) => 
            _grid = grid;

        public Path Find(Vector2Int from, Vector2Int to) =>
            Find(_grid.GetNode(from), _grid.GetNode(to));

        public bool CanReach(Node from, Node to, int withStamina, out Path path)
        {
            path = Find(from, to);
            return !path.IsEmpty && path.Stamina <= withStamina;
        }

        public bool CanReach(Node from, Node to, int withStamina)
        {
            Path path = Find(from, to);
            return !path.IsEmpty && path.Stamina <= withStamina;
        }

        private Path ReconstructPath(Dictionary<Node, Node> cameFrom, Node start, Node end)
        {
            List<Node> nodes = new();
            Node current = end;

            while (current != start)
            {
                nodes.Add(current);
                current = cameFrom[current];
            }

            nodes.Add(start);
            nodes.Reverse();

            return new Path(nodes);
        }

        private Path Find(Node start, Node end)
        {
            if (start == end)
                return Path.Empty;
            
            Queue<Node> queue = new();
            HashSet<Node> visited = new();
            Dictionary<Node, Node> cameFrom = new();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                if (current == end)
                    return ReconstructPath(cameFrom, start, end);

                foreach (Node neighbor in current.Neighbours)
                {
                    if (!visited.Contains(neighbor) && !neighbor.IsOccupied)
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        cameFrom[neighbor] = current;
                    }
                }
            }

            return Path.Empty;
        }
    }
}