using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Shop/Amélioration")]
public class Améliorations : ShopItem
{
    public bool isTop = true;
    public int healthAmount = 0;
    public int manaAmount = 0;
    public int speedAmount = 0;
    public int damageAmount = 0;

    public override void ApplyEffect(PlayerManager player)
    {
        player.attack.GainHealth(healthAmount);
        player.attack.GainMana(manaAmount);
        player.mouv.GainSpeed(speedAmount);
        player.attack.GainDamage(damageAmount);
    }
}
