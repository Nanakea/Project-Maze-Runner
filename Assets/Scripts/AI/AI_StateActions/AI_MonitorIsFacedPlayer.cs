using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_StateActions/AI_MonitorIsFacedPlayer")]
    public class AI_MonitorIsFacedPlayer : AI_StateAction
    {
        public float enemyFacedPlayerAngle;
        public float enemyFacedPlayerDis;
        public float aggroTransitionWaitRate;

        public override void Tick(AIStateManager aiStates)
        {
            AIManager ai = aiStates.ai;
            if(ai.angleToPlayer <= enemyFacedPlayerAngle)
            {
                if(ai.disToPlayer <= enemyFacedPlayerDis)
                {
                    aiStates.aggroTransitionWaitTimer += aiStates.delta;
                    if(aiStates.aggroTransitionWaitTimer >= aggroTransitionWaitRate)
                    {
                        aiStates.isFacedPlayer = true;
                    }
                }
            }
        }
    }
}