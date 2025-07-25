using UnityEngine;

namespace _Project
{
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }
        internal Node[,] Nodes { get; }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            Nodes = new Node[width, height];
        }

        public void SetNode(int x, int y, Node node) => 
            Nodes[x, y] = node;

        public Node GetNode(int x, int y) => 
            Nodes[x, y];

        public bool IsOccupied(Vector2Int position) => 
            Nodes[position.x, position.y].IsOccupied;

        public Node GetNode(Vector2Int position) => 
            Nodes[position.x, position.y];

        public Vector2Int GetPosition(Node node)
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    if (Nodes[x, y] == node)
                        return new Vector2Int(x, y);
            
            throw new System.Exception($"There is no such node {node}");
        }
    }
}
