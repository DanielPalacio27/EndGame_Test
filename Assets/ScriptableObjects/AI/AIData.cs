using UnityEngine;

[CreateAssetMenu]
public class AIData : ScriptableObject
{
    public float speed = 2.0f;
    public float distanceToDetectEnemy = 6f;
    public float distanceToShoot = 4f;
    public LayerMask playerLayer = 0;
}
