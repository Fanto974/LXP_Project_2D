using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewComp", menuName = "Shop/Compétence")]
public class Competence : ShopItem
{
    public AttackEffect AttackEffect;

    public override void ApplyEffect(PlayerManager player)
    {
        if (AttackEffect != null)
        {
            player.attack.attackEffects.Add(AttackEffect);
            Debug.Log($"Compétence {itemName} ajoutée au joueur !");
        }
    }
}
