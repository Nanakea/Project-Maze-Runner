using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/MonitorisFacedEnemy")]
    public class MonitorisFacedEnemy : StateAction
    {
        public float rotateSpeed = 5;
        public float isFaceEnemyThershold = 5;

        public override void Tick(StateManager states)
        {
            Vector3 dirToEnemy = states.vector3Zero;

            if (states.currentEnemyStates != null)
                dirToEnemy = states.currentEnemyStates.mTransform.position - states.mTransform.position;
            else
                dirToEnemy = states.playerEnemyStates.mTransform.position - states.mTransform.position;

            dirToEnemy.y = 0;

            if (dirToEnemy == states.vector3Zero)
                dirToEnemy = states.mTransform.forward;

            Quaternion lookRotation = Quaternion.LookRotation(dirToEnemy);
            states.mTransform.rotation = Quaternion.Slerp(states.mTransform.rotation, lookRotation, states.delta * rotateSpeed);

            if (Vector3.Angle(states.mTransform.forward, dirToEnemy) <= isFaceEnemyThershold)
            {
                states.isFacedEnemy = true;
            }
        }
    }
}