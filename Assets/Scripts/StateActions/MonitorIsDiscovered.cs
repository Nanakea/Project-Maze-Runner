using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/MonitorIsDiscovered")]
    public class MonitorIsDiscovered : StateAction
    {
        public override void Tick(StateManager states)
        {
            if (states.currentEnemyStates != null)
            {
                states.isDiscovered = true;
            }
        }
    }
}