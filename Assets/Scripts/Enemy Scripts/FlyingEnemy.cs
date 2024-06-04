using UnityEngine;

namespace Enemy_Scripts
{
    public class FlyingEnemy : Enemy 
    {
        private new void Awake()
        {
            base.Awake();
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