using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_StateActions/AIPatrolOnMarks")]
    public class AIPatrolOnMarks : AI_StateAction
    {
        [Space(5)]
        public Vector3 destinationPosition;
        public float stopDistanceProportion = 0.2f;
        public float turnSpeedThreshold = 0.5f;
        public float slowingSpeed = 0.175f;
        public float turnSmoothing = 15f;
        public float aiSearchingWaitForSeconds = 2;
        public float searchingTimer;

        public bool goingBackToFirstSpot;

        int i;
        public NavMeshAgent agent;

        public override void Tick(AIStateManager aiStates)
        {
            // Init
            if(!aiStates.IsPatrolInited)
            {
                i = -1;
                searchingTimer = 0;

                agent = aiStates.ai.agent;
                agent.updateRotation = false;
                agent.stoppingDistance = 1;

                destinationPosition = aiStates.mTransform.position;
                agent.SetDestination(destinationPosition);

                aiStates.IsPatrolInited = true;
            }

            if (agent.pathPending)
                return;

            float speed = agent.desiredVelocity.magnitude;

            //Debug.Log("Agent Not PathPending");

            if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
            {
                //Debug.Log("Stopping");
                Stopping(out speed, aiStates);
            }
            else if(agent.remainingDistance <= agent.stoppingDistance)
            {
                //Debug.Log("Slowing");
                Slowing(out speed, agent.remainingDistance, aiStates);
            }
            else if(speed > turnSpeedThreshold)
            {
                //Debug.Log("Turning");
                Turning(aiStates);
            }
        }

        private void Stopping(out float speed, AIStateManager aiState)
        {
            agent.isStopped = true;

            speed = 0;

            if (WaitForSearching(aiState) == false)
                return;
        }

        private void Slowing(out float speed, float distanceToDestination, AIStateManager aiState)
        {
            agent.isStopped = true;
            float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;
            aiState.mTransform.position = Vector3.MoveTowards(aiState.mTransform.position, destinationPosition, slowingSpeed * aiState.delta);
            speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);
        }

        private void Turning(AIStateManager aiState)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
            aiState.mTransform.rotation = Quaternion.Lerp(aiState.mTransform.rotation, targetRotation, turnSmoothing * aiState.delta);
        }

        public void GetNextDestination(AIStateManager aiState)
        {
            if(i >= aiState.currentPartrolPointList.patrolPoints.Count - 1)
            {
                goingBackToFirstSpot = true;
            }

            if(i <= 0)
            {
                goingBackToFirstSpot = false;
            }

            if(goingBackToFirstSpot)
            {
                i--;
            }
            else
            {
                i++;
            }

            searchingTimer = 0;
            destinationPosition = aiState.currentPartrolPointList.patrolPoints[i].pos;
            agent.SetDestination(destinationPosition);
            agent.isStopped = false;
        }

        private bool WaitForSearching(AIStateManager aiState)
        {
            bool retVal = false;

            searchingTimer += aiState.delta;
            //Debug.Log("searching...");
            if (searchingTimer >= aiSearchingWaitForSeconds)
            {
                retVal = true;
                GetNextDestination(aiState);
            }

            return retVal;
        }
    }
}