using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/MonitorPlayerEnemy")]
    public class MonitorPlayerEnemy : StateAction
    {
        public float aggroAngle = 45;
        public float aggroDistance;

        public override void Tick(StateManager states)
        {
            int raycastLayerMask = 0;

            if (states.isDiscoveredPlayerEnemy)
                return;
            
            if (states.player_1)
                raycastLayerMask = 1 << 10;
            else
                raycastLayerMask = 1 << 9;

            int result = Physics.OverlapSphereNonAlloc(states.mTransform.position, aggroDistance, states.playerCols, raycastLayerMask);
            if (result > 0)
            {
                Vector3 dirToCol = states.playerCols[0].transform.position - states.mTransform.position;
                dirToCol.y = 0;

                float angleToCol = Vector3.Angle(states.mTransform.forward, dirToCol);
                if (angleToCol <= aggroAngle)
                {
                    RaycastHit hit;
                    Debug.DrawRay(states.mTransform.position, dirToCol, Color.red);
                    if (Physics.Raycast(states.mTransform.position, dirToCol, out hit, aggroDistance, raycastLayerMask))
                    {
                        states.isDiscoveredPlayerEnemy = true;
                        states.playerEnemyStates = states.playerCols[0].GetComponent<StateManager>();

                        states.playerEnemyStates.playerEnemyStates = states;
                        states.playerCols[0] = null;
                    }
                }
            }
        }
    }
}