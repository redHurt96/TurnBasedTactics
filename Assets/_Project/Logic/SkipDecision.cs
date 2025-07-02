using Cysharp.Threading.Tasks;

namespace _Project
{
    internal class SkipDecision : IDecision
    {
        public SkipDecision(Character source)
        {
        }

        public UniTask Execute()
        {
            return UniTask.CompletedTask;
        }
    }
}