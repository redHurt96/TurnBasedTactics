using Cysharp.Threading.Tasks;

namespace _Project
{
    public interface IDecisionMaker
    {
        UniTask<IDecision> Execute(Character source, CharactersRepository enemies);
    }
}