using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class AI_Transition : ScriptableObject
    {
        public AI_State previousState;
        public AI_State forwardState;

        public abstract void CheckAI_Transition(AIStateManager aiStates);
    }
}