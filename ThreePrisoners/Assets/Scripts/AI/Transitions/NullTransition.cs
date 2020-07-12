public class NullTransition : AIStateTransition
{
    /*
    Transition that will always return false. Used for states without any
    outgoing transitions (end states).
    */
    public bool CheckCondition()
    {
        return false;
    }

}