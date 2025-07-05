using UnityEngine;
using UnityEngine.UI;

namespace _Project.View
{
    public class CharacterUiView : MonoBehaviour
    {
        [SerializeField] private Image _health;
        [SerializeField] private Image _stamina;
        
        private Character _character;
        private int _originHealth;

        public void Setup(Character character)
        {
            _character = character;
            _originHealth = character.Health;
        }

        private void Update() => 
            transform.rotation = Quaternion.Euler(Vector3.back);

        public void UpdateHealth() => 
            _health.fillAmount = (float)_character.Health / _originHealth;

        public void UpdateStamina() => 
            _stamina.fillAmount = (float)_character.Stamina / _character.OriginStamina;
    }
}