using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_StateActions/AIWalkTowardsPlayer")]
    public class AIWalkTowardsPlayer : AI_StateAction
    {
        public Vector3 destinationPosition;
        public float stopDistance;
        public float stopDistanceProportion = 0.2f;
        public float slowingSpeed = 0.175f;
        public float turnSmoothing = 15f;
        public float agentSpeed;

        NavMeshAgent agent;

        public override void Tick(AIStateManager aiStates)
        {
            agent = aiStates.ai.agent;
            agent.updateRotation = false;
            agent.isStopped = false;
            agent.stoppingDistance = stopDistance;
            agent.speed = agentSpeed;

            destinationPosition = aiStates.currentPlayerStates.mTransform.position;
            agent.SetDestination(destinationPosition);

            if (agent.pathPending)
                return;

            float speed = agent.desiredVelocity.magnitude;

            if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
            {
                //Debug.Log("Stopping");
                Stopping(out speed, aiStates);
            }
            else if (agent.remainingDistance <= agent.stoppingDistance)
            {
                //Debug.Log("Slowing");
                Slowing(out speed, agent.remainingDistance, aiStates);
            }
        }

        private void Stopping(out float speed, AIStateManager aiState)
        {
            agent.isStopped = true;
            speed = 0;
        }

        private void Slowing(out float speed, float distanceToDestination, AIStateManager aiState)
        {
            agent.isStopped = true;
            float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;
            aiState.mTransform.position = Vector3.MoveTowards(aiState.mTransform.position, destinationPosition, slowingSpeed * aiState.delta);
            speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);
        }
    }
}