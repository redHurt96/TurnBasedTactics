using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private GameObject _outline;
        [SerializeField] private GameObject _targetOutline;
        
        public void SetColor(Color color) => 
            _renderer.material.color = color;

        public void ShowOutline() => 
            _outline.SetActive(true);
        
        public void ShowAsTarget() => 
            _targetOutline.SetActive(true);

        public void HideOutline() => 
            _outline.SetActive(false);

        public void HideTargetOutline() =>
            _targetOutline.SetActive(false);

        public async UniTask Move(Vector3 to) => 
            await transform
                .DOMove(to, .5f)
                .AsyncWaitForCompletion()
                .AsUniTask();
    }
}