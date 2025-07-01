using Cysharp.Threading.Tasks;

namespace _Project
{
    public class ErrorDecision : IDecision
    {
        public UniTask Execute() => 
            throw new($"Error decision");
    }
}