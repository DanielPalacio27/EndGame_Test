using UnityEngine;

[CreateAssetMenu()]
public class WeaponData : ItemData
{
    public float fireRate = 1.0f;
    public float Damage = 10.0f;
    public float shootForce = 500.0f;
    public float range = 10.0f;

    public AudioClip shootAudio = null;
}
