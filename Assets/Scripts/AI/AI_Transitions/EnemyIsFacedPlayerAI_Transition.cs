using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_Transitions/EnemyIsFacedPlayerAI_Transition")]
    public class EnemyIsFacedPlayerAI_Transition : AI_Transition
    {
        public override void CheckAI_Transition(AIStateManager aiStates)
        {
            if(aiStates.isFacedPlayer)
            {
                aiStates.currentState = forwardState;

                aiStates.currentPlayerStates.currentEnemyStates = aiStates;
            }
        }
    }
}