using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    public string itemName;
    public int price;
    public Sprite icon;
    public bool isTheEffectDefinitive;
    public bool isManaCompetences;
    //public GameObject prefab; // si l’objet doit être instancié

    public abstract void ApplyEffect(PlayerManager player);
}