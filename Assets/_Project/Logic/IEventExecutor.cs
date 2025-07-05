using Cysharp.Threading.Tasks;

namespace _Project
{
    public interface IEventSubscriber
    {}
    
    public interface IEventExecutor<in T> : IEventSubscriber
    {
        UniTask Execute(T @event);
    }
}