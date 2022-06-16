using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_State")]
    public class AI_State : ScriptableObject
    {
        [Header("Transition For Next State")]
        public AI_Transition forwardTransition;

        [Header("Tick StateAction")]
        public AI_StateAction[] tickedStateActions;

        [Header("FixedTick StateAction")]
        public AI_StateAction[] fixedTickedStateActions;

        public void Tick(AIStateManager aiStates)
        {
            int tickedStateActionsLength = tickedStateActions.Length;
            for (int i = 0; i < tickedStateActionsLength; i++)
            {
                tickedStateActions[i].Tick(aiStates);
            }
        }

        public void FixedTick(AIStateManager aiStates)
        {
            int fixedTickedStateActionsLength = fixedTickedStateActions.Length;
            for (int i = 0; i < fixedTickedStateActionsLength; i++)
            {
                fixedTickedStateActions[i].Tick(aiStates);
            }
        }
    }
}