using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Transitions/isDiscovered_Transition")]
    public class isDiscovered_Transition : Transition
    {
        public override void Check_Transition(StateManager states)
        {
            if (states.isDiscovered)
            {
                states.currentState = forwardState;
                states.gameObject.layer = 0;
            }
        }
    }
}