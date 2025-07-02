using _Pathfinding.Common;
using UnityEngine;

namespace _Project
{
    public class Node
    {
        public Vector2Int Position;
        public Node[] Neighbours { get; set; }
        public IGridItem Occupant { get; private set; }

        public bool IsOccupied => Occupant != null;

        public void Occupy(IGridItem item) => 
            Occupant = item;

        public void SetFree() => 
            Occupant = null;
    }
}