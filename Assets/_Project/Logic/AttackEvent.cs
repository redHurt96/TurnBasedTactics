namespace _Project
{
    public class AttackEvent
    {
        public readonly Character Target;

        public AttackEvent(Character target)
        {
            Target = target;
        }
    }
}