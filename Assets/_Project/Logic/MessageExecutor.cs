using Cysharp.Threading.Tasks;

namespace _Project
{
    public class MessageExecutor
    {
        private readonly IEventSubscriber[] _subscribers;

        public MessageExecutor(IEventSubscriber[] subscribers) => 
            _subscribers = subscribers;

        public async UniTask Execute<T>(T @event)
        {
            foreach (IEventSubscriber subscriber in _subscribers)
                if (subscriber is IEventSubscriber<T> exactSubscriber)
                    await exactSubscriber.Execute(@event);
        }
    }
}