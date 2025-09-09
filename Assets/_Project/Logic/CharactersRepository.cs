using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace _Project
{
    public class CharactersRepository
    {
        public IEnumerable<Character> All => _characters;
        public readonly int Team;

        private readonly List<Character> _characters = new();

        public CharactersRepository(int team) => 
            Team = team;

        public void Register(Character player) =>
            _characters.Add(player);

        public Queue<Character> GetInitiativeQueue() =>
            new(_characters
                .GroupBy(x => x.Initiative)
                .OrderByDescending(x => x.Key)
                .SelectMany(x => x.OrderBy(_ => Random.value)));

        public void ForEach(Action<Character> action)
        {
            foreach (var character in _characters)
                action(character);
        }

        public bool Any() => 
            _characters.Any();

        public void Remove(Character target) => 
            _characters.Remove(target);
    }
}