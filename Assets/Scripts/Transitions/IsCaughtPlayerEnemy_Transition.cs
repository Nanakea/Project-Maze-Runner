using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Transitions/IsCaughtPlayerEnemy_Transition")]
    public class IsCaughtPlayerEnemy_Transition : Transition
    {
        public override void Check_Transition(StateManager states)
        {
            if (states.isCaughtPlayerEnemy)
            {
                states.playerEnemyStates.isDiscovered = true;
                states.playerEnemyStates.render.material.color = states.isCaughtColor;
            }
        }
    }
}