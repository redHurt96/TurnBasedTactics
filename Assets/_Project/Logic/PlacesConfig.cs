using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(menuName = "Create PlacesConfig", fileName = "PlacesConfig", order = 0)]
    public class PlacesConfig : ScriptableObject
    {
        public Color Color;
        public CharacterPlace[] Places;
    }
}