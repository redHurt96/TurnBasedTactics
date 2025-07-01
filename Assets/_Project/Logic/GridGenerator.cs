using System;
using _Project;
using UnityEngine;
using Zenject;
using Grid = _Project.Grid;

namespace _Pathfinding.Common
{
    public class GridGenerator : IFactory<Grid>
    {
        private readonly int _x;
        private readonly int _y;

        public GridGenerator(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Grid Create()
        {
            Grid grid = new Grid(_x, _y);
            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    Node node = new()
                    {
                        Position = new Vector2Int(i, j)
                    };
                    grid.SetNode(i, j, node);
                }
            }

            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    Node node = grid.GetNode(i, j);
                    node.Neighbours = GetNeighbours(grid, i, j);
                }
            }
            
            return grid;
        }

        private Node[] GetNeighbours(Grid grid, int x, int y)
        {
            Node[] neighbours = new Node[4];
            int index = 0;

            if (x > 0)
            {
                neighbours[index++] = grid.GetNode(x - 1, y);
            }

            if (x < grid.Width - 1)
            {
                neighbours[index++] = grid.GetNode(x + 1, y);
            }

            if (y > 0)
            {
                neighbours[index++] = grid.GetNode(x, y - 1);
            }

            if (y < grid.Height - 1)
            {
                neighbours[index++] = grid.GetNode(x, y + 1);
            }

            Node[] result = new Node[index];
            Array.Copy(neighbours, result, index);
            return result;
        }
    }
}