using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_StateActions/UpdateDirDisAngle2Player")]
    public class UpdateDirDisAngle2Player : AI_StateAction
    {
        public override void Tick(AIStateManager aiStates)
        {
            AIManager ai = aiStates.ai;

            if (aiStates.currentPlayerStates == null)
                return;

            Vector3 dirToPlayer = aiStates.currentPlayerStates.mTransform.position - aiStates.mTransform.position;
            dirToPlayer.y = 0;
            if (dirToPlayer == aiStates.vector3Zero)
                dirToPlayer = aiStates.mTransform.forward;

            ai.dirToPlayer = dirToPlayer;

            ai.disToPlayer = Vector3.Magnitude(dirToPlayer);

            Vector3 forwardAIVector3 = aiStates.mTransform.forward;
            forwardAIVector3.y = 0;
            ai.angleToPlayer = Vector3.Angle(forwardAIVector3, dirToPlayer);
        }
    }
}