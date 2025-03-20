using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoint = 100f;
        bool isDead = false;

        public void TakeDamage(float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            
            if(healthPoint == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)  return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}
