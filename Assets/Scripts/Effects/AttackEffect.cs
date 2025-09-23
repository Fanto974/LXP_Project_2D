using UnityEngine;

public abstract class AttackEffect : ScriptableObject
{
    public int costManaAtt;
    public abstract void Apply(IEffectReceiver target);
}