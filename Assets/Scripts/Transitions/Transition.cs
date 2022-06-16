using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class Transition : ScriptableObject
    {
        public State previousState;
        public State forwardState;

        public abstract void Check_Transition(StateManager states);
    }
}