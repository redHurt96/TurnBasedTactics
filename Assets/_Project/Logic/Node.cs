using _Pathfinding.Common;
using UnityEngine;

namespace _Project
{
    public class Node
    {
        public Vector2Int Position;
        public Node[] Neighbours { get; set; }
        
        public bool IsOccupied => _item != null;
        
        private IGridItem _item;

        public void Occupy(IGridItem item) => 
            _item = item;

        public void SetFree() => 
            _item = null;
    }
}