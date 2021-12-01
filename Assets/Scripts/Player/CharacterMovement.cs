using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement : MonoBehaviour, IMoveable
    {
        private Rigidbody mRigid = null;
        private CharacterAnimation mAnimation = null;
        private PlayerController player = null;

        void Start()
        {
            mRigid = GetComponent<Rigidbody>();
            mAnimation = GetComponent<CharacterAnimation>();
            player = GetComponent<PlayerController>();
        }
        
        public void Move(Vector2 _direction, bool _isAiming)
        {
            if (_direction != Vector2.zero && !_isAiming) //if not aiming, proceed to rotate in direction of movement
            {
                float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
                mRigid.rotation = Quaternion.RotateTowards(mRigid.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * 1000f);
            }
            
            mRigid.MovePosition(mRigid.position + new Vector3(_direction.x, 0, _direction.y) * Time.deltaTime * player.Data.speed);
            mAnimation.SetState(_direction.magnitude);
        }
    }
}
