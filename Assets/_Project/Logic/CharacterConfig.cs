using _Project.View;
using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(menuName = "Create CharacterConfig", fileName = "CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject
    {
        public Character Character;
        public CharacterView View;
    }
}