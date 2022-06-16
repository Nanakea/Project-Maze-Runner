using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Transitions/isPlayerFaceEnemy_Transition")]
    public class isPlayerFaceEnemy_Transition : Transition
    {
        public State playerEnemyState;

        public override void Check_Transition(StateManager states)
        {
            if(states.isFacedEnemy)
            {
                UIManager ui = UIManager.singleton;

                if (states.currentEnemyStates != null)
                {
                    ui.PlayUITransitionMoveIn(states);
                    ScrambleMiniGame.singleton.isPlayersBattle = false;
                }
                else
                {
                    states.playerEnemyStates.currentState = playerEnemyState;
                    ui.PlayPairUITransitionMoveIn(states, states.playerEnemyStates);
                    ScrambleMiniGame.singleton.isPlayersBattle = true;
                }

                states.currentState = forwardState;
            }
        }
    }
}