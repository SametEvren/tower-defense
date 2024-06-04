using UnityEngine;

namespace Enemy_Scripts
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        public float damage;
        public float speed;
        public float health;
        public float levelMultiplier;
    }
}