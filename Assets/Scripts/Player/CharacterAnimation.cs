using UnityEngine;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator mAnimator = null;

        private void Awake()
        {
            mAnimator = GetComponent<Animator>();
        }

        public void SetState(float _magnitude)
        {
            mAnimator.SetFloat("speed", _magnitude);
        }

        public void SetShoot(bool _isShooting)
        {
            mAnimator.SetBool("shoot", _isShooting);
        }

        /// <summary>
        /// Returns true if the current Animator State Name is the same as the given parameter
        /// </summary>
        /// <param name="_stateName"></param>
        /// <returns></returns>
        public bool CheckForCurrentState(string _stateName)
        {
            return mAnimator.GetCurrentAnimatorStateInfo(1).IsName(_stateName);
        }
    }
}
