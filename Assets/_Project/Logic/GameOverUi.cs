using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.SceneManagement.SceneManager;

namespace _Project
{
    public class GameOverUi : MonoBehaviour
    {
        [SerializeField] private Button _reload;
        [SerializeField] private TextMeshProUGUI _description;

        private void Start() => 
            _reload.onClick.AddListener(() => LoadScene(GetActiveScene().buildIndex));

        private void OnDestroy() => 
            _reload.onClick.RemoveAllListeners();

        public void Show(int winnerTeam)
        {
            gameObject.SetActive(true);
            _description.text = $"Winner team: {winnerTeam}";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}