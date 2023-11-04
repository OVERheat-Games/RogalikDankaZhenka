using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private float damageBoost = 1.0f;
    [SerializeField]
    private float fireRateBoost = 1.0f;

    public float DamageBoost
    {
        get { return damageBoost; }
    }

    public float FireRateBoost
    {
        get { return fireRateBoost; }
    }
}
