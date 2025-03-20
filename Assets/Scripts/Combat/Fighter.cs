using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Core;

namespace RPG.Combat
{

public class Fighter : MonoBehaviour , IAction
{
        Health enemytarget;
        [SerializeField] float timebetweenAttack = Mathf.Infinity;
        float timeSinceLastAttack;
        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float weaponDamage = 5.0f;
        [SerializeField] int attackAnimIndex;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (enemytarget == null) return;
            if (enemytarget.IsDead()) return;

            

            if (!GetIsRange())
            {
                GetComponent<PlayerMove>().MoveTo(enemytarget.transform.position , 1f);
            }

            else
            {
                
                GetComponent<PlayerMove>().StopMove();

                AttackBehaviour();

            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(enemytarget.transform);
            if (timeSinceLastAttack > timebetweenAttack)
            {

                GetComponent<Animator>().ResetTrigger("StopAttack");

                attackAnimIndex = Random.Range(0, 3);

                if (attackAnimIndex == 1 || attackAnimIndex == 2)
                {
                    GetComponent<Animator>().SetTrigger("Attack");
                }

                else
                {
                    GetComponent<Animator>().SetTrigger("Attack1");
                }

                timeSinceLastAttack = 0;
                Hit();
            }
        }

        private bool GetIsRange()
        {
            return Vector3.Distance(transform.position, enemytarget.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {

            if(combatTarget == null) { return false; }

            Health targetTotest = combatTarget.GetComponent<Health>();
            return targetTotest != null && !targetTotest.IsDead();

        }


        public void Attacker(GameObject combatTarget)
        {
            GetComponent<Scheduler>().StartAction(this);
            enemytarget = combatTarget.GetComponent<Health>();
            
        }

    public void Cancel()
    {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
            enemytarget = null;
            GetComponent<PlayerMove>().Cancel();
    }

        public void Hit()
        {
          if (enemytarget == null) return;
          enemytarget.TakeDamage(weaponDamage);
        }

}

}

