using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] float chaseDistance = 5f;
        NavMeshAgent nvmsh;
        GameObject playerObj;
        Fighter fighter;
        Health health;
        PlayerMove mover;
        float timeSinceSawPlayer = Mathf.Infinity;
        float timeSinceArrivedatWaypoint = Mathf.Infinity;
        [SerializeField] float suspicionTime = 3.0f;
        public PatrolPaths patrolPath;
        [SerializeField] int CurrentWaypointIndex = 0;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0,1)] 
        [SerializeField] float patrolSpeedFraction = 0.2f;

        Vector3 guardLocation;

        void Start()
        {
            playerObj = GameObject.FindWithTag("Player");
            nvmsh = GetComponent<NavMeshAgent>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            guardLocation = transform.position;
            mover = GetComponent<PlayerMove>();
        }

        
        void Update()
        {
            if (health.IsDead()) return;        

            if (isAttackRange(playerObj) && fighter.CanAttack(playerObj))
            {
                timeSinceSawPlayer = 0;
                Attack();

            }

            else if (timeSinceSawPlayer < suspicionTime)
            {
                Suspicious();
            }

            else
            {
                Patrol();
            }

            timeSinceSawPlayer += Time.deltaTime;
        }

        private void Patrol()
        {
            Vector3 nextPos = guardLocation;

            timeSinceArrivedatWaypoint += Time.deltaTime;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    CycleWaypoint();
                }

                nextPos = GetCurrentWaypoint();
            }

            if (timeSinceArrivedatWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPos , patrolSpeedFraction);
                timeSinceArrivedatWaypoint = 0;
            }

            
        }

        private bool AtWayPoint()
        {
            float distancebetweenWaypoints = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distancebetweenWaypoints < wayPointTolerance;
        }

        private void CycleWaypoint()
        {
            CurrentWaypointIndex = patrolPath.GetNextIndex(CurrentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWayPoint(CurrentWaypointIndex);
        }

        private void Suspicious()
        {
          GetComponent<Scheduler>().CancelCurrentAction();
        }
           
        private void Attack()
        {
            fighter.Attacker(playerObj);
        }

        private bool isAttackRange(GameObject playerObj)
        {
            float DistanceChase = Vector3.Distance(playerObj.transform.position, transform.position);
            return DistanceChase < chaseDistance;
        }

        private void OnDrawGizmos()
        {      
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
 