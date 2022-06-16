using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/ChasingPlayerEnemy")]
    public class ChasingPlayerEnemy : StateAction
    {
        public float stopDistanceProportion = 0.2f;
        public float turnSpeedThreshold = 0.5f;
        public float slowingSpeed = 0.175f;
        public float turnSmoothing = 15f;

        public override void Tick(StateManager states)
        {
            if (!states.isCaughtPlayerEnemy)
            {
                states.agent.isStopped = false;
                states.destinationPosition = states.playerEnemyStates.mTransform.position;
                states.agent.SetDestination(states.destinationPosition);
            }

            if (states.agent.pathPending)
                return;

            float speed = states.agent.desiredVelocity.magnitude;
            if (states.agent.remainingDistance <= states.agent.stoppingDistance * stopDistanceProportion)
            {
                //Debug.Log("Stopping");
                Stopping(out speed, states);
            }
            else if (states.agent.remainingDistance <= states.agent.stoppingDistance)
            {
                //Debug.Log("Slowing");
                Slowing(out speed, states.agent.remainingDistance, states);
            }
            else if (speed > turnSpeedThreshold)
            {
                //Debug.Log("Turning");
                Turning(states);
            }
        }

        private void Stopping(out float speed, StateManager states)
        {
            states.agent.isStopped = true;
            speed = 0;
        }

        private void Slowing(out float speed, float distanceToDestination, StateManager states)
        {
            states.agent.isStopped = true;
            float proportionalDistance = 1f - distanceToDestination / states.agent.stoppingDistance;
            states.mTransform.position = Vector3.MoveTowards(states.mTransform.position, states.destinationPosition, slowingSpeed * states.delta);
            speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);
        }

        private void Turning(StateManager states)
        {
            Vector3 lookRot = states.playerEnemyStates.mTransform.position - states.mTransform.position;
            lookRot.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lookRot);
            states.mTransform.rotation = Quaternion.Lerp(states.mTransform.rotation, targetRotation, turnSmoothing * states.delta);
        }
    }
}