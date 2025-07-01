using System;
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
        
        public int Team { get; set; }
        public Vector2Int Position { get; set; }
        
        private MessagesQueue _broker;

        public Character Copy(MessagesQueue broker)
        {
            return new Character
            {
                Name = Name,
                Initiative = Initiative,
                _broker = broker,
            };
        }

        public void Move(Vector2Int path) => 
            Position = path;

        public bool IsEnemy(Character forCharacter) => 
            Team != forCharacter.Team;
    }
}