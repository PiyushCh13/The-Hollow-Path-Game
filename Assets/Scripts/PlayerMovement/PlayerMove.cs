using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Saving;

namespace RPG.Control
{
    public class PlayerMove : MonoBehaviour , IAction , ISaveable
    {
        private NavMeshAgent navMesh;
        private Animator anim;
        Health health;
        [SerializeField] float maxSpeed;
       

        void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();
            
        }


        void Update()
        {
            navMesh.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void Move()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                MoveTo(hit.point , 1f);
            }
        }

        public void StopMove()
        {
            navMesh.isStopped = true;
        }

        public void StopMoveAction(Vector3 destination)
        {
            GetComponent<Scheduler>().StartAction(this);
            MoveTo(destination , 1f);
        }

        public void StartMoveAction(Vector3 destination , float speedFraction)
        {
            GetComponent<Scheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination , speedFraction);
        }

        public void MoveTo(Vector3 destination , float speedFraction)
        {
            navMesh.destination = destination;
            navMesh.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMesh.isStopped = false;
        }

        public void UpdateAnimator()
        {
            Vector3 velocity = navMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformVector(velocity);
            float speed = localVelocity.z;
            anim.SetFloat("Movement", speed);
        }

        public void Cancel()
        {
            navMesh.isStopped = true; 
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
