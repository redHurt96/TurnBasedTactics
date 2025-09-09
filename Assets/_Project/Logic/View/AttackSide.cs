using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;

namespace _Project.View
{
    public class AttackSide : MonoBehaviour
    {
        [field:SerializeField] public Vector2Int Direction { get; private set; }
        
        [SerializeField] private GameObject _arrow;
        
        private bool _clicked;

        private void OnMouseDown() => 
            _clicked = true;

        private void OnMouseEnter() => 
            _arrow.SetActive(true);

        private void OnMouseExit() =>
            _arrow.SetActive(false);

        public async UniTask AwaitForClick(CancellationToken token)
        {
            _clicked = false;
            Show();

            await WaitUntil(() => _clicked, cancellationToken: token);

            Hide();
            _clicked = false;
        }

        [Button]
        private void Show() => 
            gameObject.SetActive(true);

        [Button]
        private void Hide() => 
            gameObject.SetActive(false);
    }
}