using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//isDiscoveredPlayerEnemy_Transition

namespace SA
{
    [CreateAssetMenu(menuName = "Transitions/isDiscoveredPlayerEnemy_Transition")]
    public class isDiscoveredPlayerEnemy_Transition : Transition
    {
        public override void Check_Transition(StateManager states)
        {
            if (states.isDiscoveredPlayerEnemy)
            {
                states.currentState = forwardState;
                states.render.material.color = states.facingPlayerStateColor;
                states.inp.stopPlayer1_UpdateInput = true;
                states.inp.stopPlayer2_UpdateInput = true;
                states.vertical = 0;
                states.horizontal = 0;
                states.gameObject.layer = 0;
            }
        }
    }
}
