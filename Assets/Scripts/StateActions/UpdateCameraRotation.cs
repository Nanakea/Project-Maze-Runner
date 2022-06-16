using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/UpdateCameraRotation")]
    public class UpdateCameraRotation : StateAction
    {
        public override void Tick(StateManager states)
        {
            Vector2 currentMouseDelta = states.vector2Zero;
            Vector2 currentMouseDeltaVelocity = states.vector2Zero;

            Vector2 targetMouseDelta = states.vector2Zero;
            targetMouseDelta = new Vector2(states.horizontal, 0);

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, states.camController.mouseSmoothTime);
            states.mTransform.Rotate(Vector3.up * currentMouseDelta.x * states.camController.mouseSensitivity);
        }
    }
}