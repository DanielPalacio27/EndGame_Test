using UnityEngine;

namespace Character
{
    public class CharacterAiming : MonoBehaviour, IRotable
    {
        Rigidbody mRigid = null;
        CharacterAnimation mAnimation = null;
        PlayerInventory playerInventory = null;

        void Start()
        {
            mRigid = GetComponent<Rigidbody>();
            mAnimation = GetComponent<CharacterAnimation>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        public void Rotate(Vector2 _direction)
        {
            if (_direction == Vector2.zero)
            {
                mAnimation.SetShoot(false);
                return;
            }

            float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
            mRigid.rotation = Quaternion.RotateTowards(mRigid.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * 1000f);

            if (playerInventory.HasWeaponEquipped)
                mAnimation.SetShoot(true);
        }
    }
}
