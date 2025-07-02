using Cysharp.Threading.Tasks;

namespace _Project
{
    public interface IEventSubscriber
    {}
    
    public interface IEventSubscriber<in T> : IEventSubscriber
    {
        UniTask Execute(T @event);
    }
}