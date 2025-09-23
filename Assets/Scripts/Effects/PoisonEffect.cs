using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Effects/Poison")]
public class PoisonEffect : AttackEffect
{
    public int damagePerTick = 5;
    public float tickRate = 2f;
    public int numberOfTicks = 3;
    

    public override void Apply(IEffectReceiver target)
    {
        target.GetMonoBehaviour().StartCoroutine(ApplyPoison(target));
    }

    private IEnumerator ApplyPoison(IEffectReceiver target)
    {
        for (int i = 0; i < numberOfTicks; i++)
        {
            target.takeDamage(damagePerTick);
            yield return new WaitForSeconds(tickRate);
        }
    }
}
