using System;
using System.Collections.Generic;
using _Project;
using UnityEngine;
using Grid = _Project.Grid;

namespace _Pathfinding.Common
{
    public class BreathFirstPathSolver
    {
        private readonly Grid _grid;
        public event Action<Node> CellVisited;

        public BreathFirstPathSolver(Grid grid)
        {
            _grid = grid;
        }

        public List<Node> Find(Vector2Int from, Vector2Int to) =>
            Find(_grid.GetNode(from), _grid.GetNode(to));
        
        public List<Node> Find(Node start, Node end)
        {
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
                        CellVisited?.Invoke(neighbor);
                        cameFrom[neighbor] = current;
                    }

                    if (neighbor == end)
                        return ReconstructPath(cameFrom, start, end);
                }
            }

            Debug.Log("No path found.");
            return null;
        }

        private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node start, Node end)
        {
            List<Node> path = new();
            Node current = end;

            while (current != start)
            {
                path.Add(current);
                current = cameFrom[current];
            }

            path.Add(start);
            path.Reverse();

            return path;
        }

        public bool CanReach(Node from, Node to) => 
            Find(from, to) != null;
    }
}