using System.Collections.Generic;
using UnityEngine;

namespace Player_Scripts
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public int level;
        public int currentLevelExperience;
        public List<int> levelExperienceRequirements;
        public int gold;
        public int castleLevel;
        public int startingHealth;
    }
}