using System;
using System.Collections.Generic;
using _Project.View;

namespace _Project
{
    public class CharactersViewMap
    {
        private readonly Dictionary<Character, CharacterView> _map = new();
        private readonly Dictionary<Character, CharacterUiView> _uiMap = new();

        public void Register(Character character, CharacterView view, CharacterUiView uiView)
        {
            _map.Add(character, view);
            _uiMap.Add(character, uiView);
        }

        public CharacterView Get(Character character) => 
            _map[character];
        
        public CharacterUiView GetUi(Character character) => 
            _uiMap[character];

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

        public void Remove(Character character)
        {
            _map.Remove(character);
            _uiMap.Remove(character);
        }
    }
}