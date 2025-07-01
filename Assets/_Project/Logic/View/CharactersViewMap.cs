using System;
using System.Collections.Generic;

namespace _Project
{
    public class CharactersViewMap
    {
        private readonly Dictionary<Character, CharacterView> _map = new();

        public void Register(Character character, CharacterView view) => 
            _map.Add(character, view);

        public CharacterView Get(Character character) => 
            _map[character];

        public IEnumerable<CharacterView> GetEnemies(Character forCharacter)
        {
            foreach (KeyValuePair<Character, CharacterView> pair in _map)
            {
                if (pair.Key.IsEnemy(forCharacter))
                    yield return pair.Value;
            }
        }

        public void ForEachView(Action<CharacterView> action)
        {
            foreach (CharacterView view in _map.Values)
                action(view);
        }
    }
}