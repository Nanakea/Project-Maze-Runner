using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace SA
{
    public class PlayerMovement : MonoBehaviour
    {
        //public StateManager states;
        public NavMeshAgent agent;
        public float agentSpeed;
        public float turnSmoothing = 15f;
        public float slowingSpeed = 0.175f;
        public float turnSpeedThreshold = 0.5f;

        private Vector3 destinationPosition;
        private const float stopDistanceProportion = 0.1f;
        private const float navMeshSampleDistance = 4f;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            agent.updateRotation = false;
            destinationPosition = transform.position;
        }

        private void Update()
        {
            agent.speed = agentSpeed;

            if (agent.pathPending)
                return;

            float speed = agent.desiredVelocity.magnitude;

            if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
            {
                Stopping(out speed);
            }
            else if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Slowing(out speed, agent.remainingDistance);
            }

            else if (speed > turnSpeedThreshold)
            {
                Moving();
            }
        }

        private void Stopping(out float speed)
        {
            agent.isStopped = true;
            transform.position = destinationPosition;
            speed = 0f;
        }

        private void Slowing(out float speed, float distanceToDestination)
        {
            agent.isStopped = true;
            float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;
            Quaternion targetRotation = transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);
            transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);
            speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);
        }

        private void Moving()
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        }

        public void OnGroundClick(BaseEventData data)
        {
            //Debug.Log("OnGroundClick");
            PointerEventData pData = (PointerEventData)data;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(pData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
                destinationPosition = hit.position;
            else
                destinationPosition = pData.pointerCurrentRaycast.worldPosition;

            agent.SetDestination(destinationPosition);
            agent.isStopped = false;
        }
    }
}