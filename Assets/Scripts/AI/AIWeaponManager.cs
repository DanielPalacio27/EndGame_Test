using UnityEngine;

public class AIWeaponManager : MonoBehaviour
{
    [SerializeField] private Transform weaponContainer = null;
    private AIController AIController = null;
    private Weapon[] weapons = null;

    void Start()
    {
        weapons = weaponContainer.GetComponentsInChildren<Weapon>();
        AIController = GetComponent<AIController>();
        SetWeaponsShootingCondition();
    }

    /// <summary>
    /// Set the coondition for which an AI shoots
    /// </summary>
    public void SetWeaponsShootingCondition()
    {
        foreach (Weapon w in weapons)
            w.ShootingCondition = () => AIController.canShoot;
    }
}
