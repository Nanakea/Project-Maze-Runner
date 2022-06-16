using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI_ActionHolders")]
    public class AI_ActionHolder : ScriptableObject
    {
        [Header("AI Action List")]
        public AI_Action[] actionsList;

        [Header("decision Time")]
        public float decisionTime;

        private List<AI_Action> sameScoreActions;
        private AI_Action retVal;

        public void HandleDecisionsTimer(AIManager ai)
        {
            ai.decisionTimer += ai.aiStates.delta;
            if(ai.decisionTimer > decisionTime)
            {
                ai.decisionTimer = 0;
                ai.currentAction = FindTopScoreAction(ai);
            }
        }

        public AI_Action FindTopScoreAction(AIManager ai)
        {
            return null;
        }
    }
}