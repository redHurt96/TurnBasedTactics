using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project
{
    public readonly struct Path
    {
        public static Path Empty = new();
        
        public int Stamina => IsEmpty ? 0 : _path.Count - 1;
        public bool IsEmpty => _path == null || _path.Count == 0;
        public Node Last => _path[^1];

        private readonly List<Node> _path;

        public Path(List<Node> path)
        {
            _path = path;
        }

        public IEnumerable<T> Select<T>(Func<Node, T> func)
        {
            return _path.Select(func);
        }
    }
}