using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.View
{
    public class AttackSide : MonoBehaviour
    {
        [field:SerializeField] public Vector2Int Direction { get; private set; }
        
        private bool _clicked;

        private void OnMouseDown() => 
            _clicked = true;

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);

        public async UniTask AwaitForClick(CancellationToken token)
        {
            _clicked = false;

            await UniTask.WaitUntil(() => _clicked, cancellationToken: token);

            _clicked = false;
        }
    }
}