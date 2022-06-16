using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_StateActions/AIRotateFacePlayer")]
    public class AIRotateFacePlayer : AI_StateAction
    {
        public float rotateSpeed = 6;

        public override void Tick(AIStateManager aiStates)
        {
            Quaternion lookRotation = Quaternion.LookRotation(aiStates.ai.dirToPlayer);
            aiStates.mTransform.rotation = Quaternion.Slerp(aiStates.mTransform.rotation, lookRotation, aiStates.delta * rotateSpeed);
        }
    }
}