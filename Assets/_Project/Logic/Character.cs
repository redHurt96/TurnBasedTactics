using System;
using System.Collections;
using System.Collections.Generic;
using _Pathfinding.Common;
using UnityEngine;

namespace _Project
{
    [Serializable]
    public class Character : IGridItem
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public int Initiative { get; private set; }
        
        public int Team { get; private set; }
        public Node Node { get; private set; }
        public Vector2Int Position => Node.Position;

        public Character Copy(int team, Node node)
        {
            Character copy = new()
            {
                Name = Name,
                Initiative = Initiative,
                Team = team,
            };
            copy.Move(node);
            return copy;
        }

        public void Move(Node target)
        {
            Node?.SetFree();
            Node = target;
            Node.Occupy(this);
        }

        public bool IsEnemy(Character forCharacter) => 
            Team != forCharacter.Team;
    }
}