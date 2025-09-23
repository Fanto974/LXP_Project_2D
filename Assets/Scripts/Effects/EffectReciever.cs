using UnityEngine;

public interface IEffectReceiver
{
    void takeDamage(int amount);
    MonoBehaviour GetMonoBehaviour(); // utile pour lancer des coroutines
}