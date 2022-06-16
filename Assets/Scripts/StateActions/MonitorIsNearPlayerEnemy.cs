using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/MonitorIsNearPlayerEnemy")]
    public class MonitorIsNearPlayerEnemy : StateAction
    {
        public float AggroPlayerDistance = 4f;

        public override void Tick(StateManager states)
        {
            float dis = Vector3.Distance(states.mTransform.position, states.playerEnemyStates.mTransform.position);
            if (dis <= AggroPlayerDistance)
            {
                states.isCaughtPlayerEnemy = true;
                states.agent.isStopped = true;
            }
        }
    }
}