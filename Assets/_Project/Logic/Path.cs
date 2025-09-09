using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project
{
    public readonly struct Path
    {
        public static Path Invalid = new();
        
        public int Stamina => IsEmpty ? 0 : _path.Count - 1;
        public bool IsEmpty => _path == null || _path.Count == 0;
        public bool IsSameCell => _path is { Count: 1 };
        public Node Last => _path[^1];

        private readonly List<Node> _path;

        public Path(List<Node> path) => 
            _path = path;

        public Path(Node from) : this(new List<Node> { from }) {}

        public IEnumerable<T> Select<T>(Func<Node, T> func) => 
            _path.Select(func);
    }
}