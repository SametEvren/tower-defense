using UnityEngine;
using UnityEngine.UI;

namespace Enemy_Scripts
{
    public class EnemyUI : MonoBehaviour
    {
        private Enemy _enemy;
        [SerializeField] private Image healthBarFillImage;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            _enemy.EnemyHealthChanged += UpdateHealthUI;
        }

        private void UpdateHealthUI(float newHealth)
        {
            healthBarFillImage.fillAmount = _enemy.Health / _enemy.StartHealth;
        }

        private void OnDestroy()
        {
            _enemy.EnemyHealthChanged -= UpdateHealthUI;
        }
    }
}