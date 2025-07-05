using System.Threading;
using Cysharp.Threading.Tasks;
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

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);

        public async UniTask AwaitForClick(CancellationToken token)
        {
            _clicked = false;

            await WaitUntil(() => _clicked, cancellationToken: token);

            _clicked = false;
        }
    }
}