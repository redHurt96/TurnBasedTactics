using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project
{
    [RequireComponent(typeof(Button))]
    public class SkipButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        public async UniTask<bool> WaitForClick(CancellationToken token)
        {
            try
            {
                await _button.OnClickAsync(token);
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }
    }
}