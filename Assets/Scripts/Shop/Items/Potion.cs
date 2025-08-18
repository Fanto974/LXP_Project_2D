using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Shop/Soin")]
public class PotionItem : ShopItem
{
    public int healAmount = 0;
    public int manaAmount = 0;

    public override void ApplyEffect(PlayerManager player)
    {
        player.attack.Heal(healAmount);
    }
}
