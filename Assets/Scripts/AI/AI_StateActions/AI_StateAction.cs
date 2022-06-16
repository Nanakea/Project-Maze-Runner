using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class AI_StateAction : ScriptableObject
    {
        public abstract void Tick(AIStateManager aiStates);
    }
}