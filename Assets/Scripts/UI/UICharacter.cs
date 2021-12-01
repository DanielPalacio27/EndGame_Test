using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class UICharacter : MonoBehaviour
    {
        [SerializeField] private PlayerController player = null;
        [SerializeField] private Image healthSlider = null;
        [SerializeField] private TMPro.TextMeshProUGUI nameText = null;

        void Awake()
        {
            player.OnTakeDamage += UpdateHealth;
            InitializePlayerUI();
        }
        private void OnDestroy()
        {
            player.OnTakeDamage -= UpdateHealth;
        }

        private void InitializePlayerUI()
        {
            nameText.text = player.Data.characterName;
        }

        private void UpdateHealth(float _currentHealth)
        {
            healthSlider.fillAmount = _currentHealth / player.Data.health;
        }
    }
}
