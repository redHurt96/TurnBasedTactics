namespace _Project
{
    public class ErrorDecision : IDecision
    {
        public void Execute() => 
            throw new($"Error decision");
    }
}