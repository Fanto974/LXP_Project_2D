using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Shop/Potion")]
public class PotionItem : ShopItem
{
    public int healAmount = 0;
    public int manaAmount = 0;
    public float speed = 0;

    public override void ApplyEffect(PlayerManager player)
    {
        player.attack.Heal(healAmount);
        player.mouv.GainSpeed(speed);
    }
}
