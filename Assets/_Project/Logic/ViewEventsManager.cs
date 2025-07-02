using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace _Project
{
    public class ViewEventsManager : IDisposable
    {
        private bool _isRunning;
        
        private readonly MessagesQueue _messagesQueue;
        private readonly MessageExecutor _messageExecutor;

        public ViewEventsManager(MessagesQueue messagesQueue, MessageExecutor messageExecutor)
        {
            _messagesQueue = messagesQueue;
            _messageExecutor = messageExecutor;
        }

        public void Initialize()
        {
            _messagesQueue.Added += TryRun;
        }

        public void Dispose()
        {
            _messagesQueue.Added -= TryRun;
        }

        private void TryRun()
        {
            if (_isRunning)
                return;

            Run();
        }

        private async UniTask Run()
        {
            _isRunning = true;
            
            while (_messagesQueue.Messages.Any())
            {
                (Type, object) pair = _messagesQueue.Messages.Dequeue();
                
                if (pair.Item1 == typeof(SelectEvent))
                    await _messageExecutor.Execute(pair.Item2 as SelectEvent);
                else if (pair.Item1 == typeof(DeselectEvent))
                    await _messageExecutor.Execute(pair.Item2 as DeselectEvent);
                else if (pair.Item1 == typeof(MoveEvent))
                    await _messageExecutor.Execute(pair.Item2 as MoveEvent);
            }
            
            _isRunning = false;
        }
    }
}