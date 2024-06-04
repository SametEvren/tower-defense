using UnityEngine;

namespace Enemy_Scripts
{
    public class WalkingEnemy : Enemy
    {
        private new void Awake()
        {
            base.Awake();
            Debug.Log("aaa");
        }
    
        public override void Move()
        {
            throw new System.NotImplementedException();
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}