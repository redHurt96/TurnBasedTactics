using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace _Project
{
    public class ViewEventsManager : IDisposable
    {
        private bool _isRunning;
        
        private readonly ViewEventsQueue _viewEventsQueue;
        private readonly MessageExecutor _messageExecutor;

        public ViewEventsManager(ViewEventsQueue viewEventsQueue, MessageExecutor messageExecutor)
        {
            _viewEventsQueue = viewEventsQueue;
            _messageExecutor = messageExecutor;
        }

        public void Initialize() => 
            _viewEventsQueue.Added += TryRun;

        public void Dispose() => 
            _viewEventsQueue.Added -= TryRun;

        private void TryRun()
        {
            if (_isRunning)
                return;

            Run();
        }

        private async UniTask Run()
        {
            _isRunning = true;
            
            while (_viewEventsQueue.Messages.Any())
            {
                (Type, object) pair = _viewEventsQueue.Messages.Dequeue();
                
                if (pair.Item1 == typeof(SelectEvent))
                    await _messageExecutor.Execute(pair.Item2 as SelectEvent);
                else if (pair.Item1 == typeof(DeselectEvent))
                    await _messageExecutor.Execute(pair.Item2 as DeselectEvent);
                else if (pair.Item1 == typeof(MoveEvent))
                    await _messageExecutor.Execute(pair.Item2 as MoveEvent);
                else if (pair.Item1 == typeof(AttackEvent))
                    await _messageExecutor.Execute(pair.Item2 as AttackEvent);
                else if (pair.Item1 == typeof(DieEvent))
                    await _messageExecutor.Execute(pair.Item2 as DieEvent);
            }
            
            _isRunning = false;
        }
    }
}