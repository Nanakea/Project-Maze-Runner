using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_StateActions/AIManagerTick")]
    public class AIManagerTick : AI_StateAction
    {
        public override void Tick(AIStateManager aiStates)
        {
            aiStates.ai.Tick();
        }
    }
}