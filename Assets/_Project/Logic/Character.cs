using System;
using System.Collections.Generic;
using _Pathfinding.Common;
using UnityEngine;
using static System.Guid;

namespace _Project
{
    [Serializable]
    public class Character : IGridItem
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public int Initiative { get; private set; }
        [field:SerializeField] public int Damage { get; private set; }
        [field:SerializeField] public int AttackStamina { get; private set; }
        [field:SerializeField] public int Health { get; private set; }
        [field:SerializeField] public int OriginStamina { get; private set; }
        
        public int Stamina { get; private set; }
        public int Team { get; private set; }
        public Node Node { get; private set; }
        public Vector2Int Position => Node.Position;
        public bool IsDead => Health <= 0;

        public Character Copy(int team, Node node)
        {
            Character copy = new()
            {
                Name = Name + NewGuid().ToString()[..5],
                Initiative = Initiative,
                Team = team,
                Damage = Damage,
                Health = Health,
                OriginStamina = OriginStamina,
                AttackStamina = AttackStamina,
            };
            copy.Move(new Path(new List<Node> { node }));
            return copy;
        }

        public void Move(Path path)
        {
            Node?.SetFree();
            Node = path.Last;
            Node.Occupy(this);
            Stamina = Mathf.Max(Stamina - path.Stamina, 0);
        }

        public bool IsEnemy(Character forCharacter) => 
            Team != forCharacter.Team;

        public void Attack(Character target)
        {
            target.TakeDamage(Damage);
            Stamina = Mathf.Max(Stamina - AttackStamina, 0);
        }

        public void RestoreStamina() => 
            Stamina = OriginStamina;

        private void TakeDamage(int damage) => 
            Health = Mathf.Max(Health - damage, 0);
    }
}