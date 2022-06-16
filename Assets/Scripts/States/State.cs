using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State")]
    public class State : ScriptableObject
    {
        [Header("Transition For Next State")]
        public Transition[] forwardTransitions;

        [Header("Tick StateAction")]
        public StateAction[] tickedStateActions;

        [Header("FixedTick StateAction")]
        public StateAction[] fixedTickedStateActions;

        public void Tick(StateManager states)
        {
            int tickedStateActionsLength = tickedStateActions.Length;
            for (int i = 0; i < tickedStateActionsLength; i++)
            {
                tickedStateActions[i].Tick(states);
            }
        }

        public void FixedTick(StateManager states)
        {
            int fixedTickedStateActionsLength = fixedTickedStateActions.Length;
            for (int i = 0; i < fixedTickedStateActionsLength; i++)
            {
                fixedTickedStateActions[i].Tick(states);
            }
        }

        public void CheckTransitions(StateManager states)
        {
            int transitionsLength = forwardTransitions.Length;
            for (int i = 0; i < transitionsLength; i++)
            {
                forwardTransitions[i].Check_Transition(states);
            }
        }
    }
}