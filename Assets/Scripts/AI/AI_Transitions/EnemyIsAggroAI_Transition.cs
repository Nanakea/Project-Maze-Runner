using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_Transitions/EnemyIsAggroAI_Transition")]
    public class EnemyIsAggroAI_Transition : AI_Transition
    {
        public override void CheckAI_Transition(AIStateManager aiStates)
        {
            if(aiStates.isAggro)
            {
                aiStates.currentState = forwardState;
                aiStates.ai.agent.isStopped = false;
            }
        }
    }
}