using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_StateActions/AI_MonitorAggro")]
    public class AI_MonitorAggro : AI_StateAction
    {
        public float aggroAngle = 45;

        Collider[] playerCols = new Collider[2];

        public override void Tick(AIStateManager aiStates)
        {
            AIManager aiManager = aiStates.ai;
            int raycastLayerMask = 1 << 9 | 1 << 10;
            if (aiStates.currentPlayerStates != null)
                return;

            int result = Physics.OverlapSphereNonAlloc(aiStates.mTransform.position, aiManager.aggroThershold, playerCols, raycastLayerMask);
            if (result > 0)
            {
                for (int i = 0; i < playerCols.Length; i++)
                {
                    if (playerCols[i] == null)
                        return;

                    Vector3 dirToCol = playerCols[i].transform.position - aiStates.mTransform.position;
                    dirToCol.y = 0;

                    float angleToCol = Vector3.Angle(aiStates.mTransform.forward, dirToCol);
                    if (angleToCol <= aggroAngle)
                    {
                        RaycastHit hit;
                        Debug.DrawRay(aiStates.mTransform.position, dirToCol, Color.red);
                        if (Physics.Raycast(aiStates.mTransform.position, dirToCol, out hit, aiManager.aggroThershold, raycastLayerMask))
                        {
                            aiStates.isAggro = true;
                            aiStates.currentPlayerStates = playerCols[i].GetComponent<StateManager>();
                            for (int j = 0; j < playerCols.Length; j++)
                            {
                                playerCols[j] = null;
                            }

                            break;
                        }
                    }
                }
            }
        }
    }
}