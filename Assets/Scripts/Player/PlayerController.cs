using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterData mData = null;
        public CharacterData Data { get => mData; set => mData = value; }

        private float currentHealth = 0;
        public event System.Action<float> OnTakeDamage = null;
        public event System.Action OnDeath = null;

        void Awake()
        {
            currentHealth = mData.health;
        }

        public void Damage(int _damage)
        {
            currentHealth -= _damage;
            currentHealth = (currentHealth <= 0) ? currentHealth = 0 : currentHealth;
            OnTakeDamage?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                OnDeath?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
