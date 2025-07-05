namespace _Project
{
    public class AttackEvent
    {
        public readonly Character Source;
        public readonly Character Target;

        public AttackEvent(Character source, Character target)
        {
            Source = source;
            Target = target;
        }
    }
}