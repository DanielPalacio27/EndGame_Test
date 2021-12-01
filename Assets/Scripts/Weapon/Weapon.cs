using UnityEngine;
using Character;

public class Weapon : Item, IShooteable
{
    [SerializeField] private WeaponData data = null;
    [SerializeField] private Transform shootReference = null;

    private CharacterInput input = null;
    private CharacterAnimation animator = null;

    private WeaponFX m_weaponFX =  null;
    private float nextTimeToFire = 0.0f;    

    public System.Func<bool> ShootingCondition = null;

    protected override void Awake()
    {
        base.Awake();
        m_weaponFX = GetComponent<WeaponFX>();
        itemData = data;
        InitializeItem();        
    }
    void FixedUpdate()
    {
        if (ShootingCondition != null && ShootingCondition.Invoke())
            Shoot();
    }

    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + data.fireRate;
            Bullet currentBullet = GetBullet();
            currentBullet.SetBullet(data.range, shootReference.position);
            currentBullet.mRigidbody.AddForce(shootReference.forward * Time.deltaTime * data.shootForce, ForceMode.Impulse);
            m_weaponFX.PlayFX(data.shootAudio);
        }
    }

    public Bullet GetBullet()
    {
        Bullet currentBullet = BulletPool.Instance.currentItem;

        currentBullet.gameObject.SetActive(true);
        currentBullet.mRigidbody.velocity = Vector3.zero;
        currentBullet.transform.position = shootReference.position;
        currentBullet.transform.rotation = shootReference.rotation;

        return currentBullet;
    }

    public override void OnPickUp(PlayerInventory _playerInventory)
    {
        base.OnPickUp(_playerInventory);
        input = _playerInventory.GetComponent<CharacterInput>();
        animator = _playerInventory.GetComponent<CharacterAnimation>();
        ShootingCondition = () => wasPickedUp && input.IsAiming
            && animator.CheckForCurrentState("Shooting");
    }
}
