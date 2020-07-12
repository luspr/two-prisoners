public class TemporaryAttributeModification : AttributeModification
{
    protected float duration;

    public TemporaryAttributeModification(string name, float duration) : base(name)
    {
        this.duration = duration;
    }

    public override bool IsActive()
    {
        return duration >= 0f;
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;
        if (!this.IsActive())
        {
            this.RemoveModificationFromCharacter();
        }
    }
}