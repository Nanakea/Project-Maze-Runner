using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/PlayerRotateFacedEnemy")]
    public class PlayerRotateFacedEnemy : StateAction
    {
        public float rotateSpeed = 5;

        public override void Tick(StateManager states)
        {
            Vector3 dirToEnemy = states.currentEnemyStates.mTransform.position - states.mTransform.position;
            dirToEnemy.y = 0;

            if (dirToEnemy == states.vector3Zero)
                dirToEnemy = states.mTransform.forward;

            Quaternion lookRotation = Quaternion.LookRotation(dirToEnemy);
            states.mTransform.rotation = Quaternion.Slerp(states.mTransform.rotation, lookRotation, states.delta * rotateSpeed);
        }
    }
}