using Cysharp.Threading.Tasks;

namespace _Project
{
    public interface IDecision
    {
        UniTask Execute();
    }
}