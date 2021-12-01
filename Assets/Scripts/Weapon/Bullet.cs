using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData data = null;
    [HideInInspector] public Rigidbody mRigidbody = null;
    private Vector3 initialPos = Vector3.zero;
    private float range = 10.0f;

    void Awake() => mRigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        DestroyAtDistance();
    }

    public void SetBullet(float _range, Vector3 _initialPos)
    {
        range = _range;
        initialPos = _initialPos;
    }

    /// <summary>
    /// Destroy the bullet at a certain distance traveled
    /// </summary>
    public void DestroyAtDistance()
    {
        if ((initialPos - transform.position).sqrMagnitude >= range)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable iDamageable = other.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
            iDamageable.Damage(data.damage);

        gameObject.SetActive(false);
    }
}
