using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/ShowCombatCommandsUI")]
    public class ShowCombatCommandsUI : StateAction
    {
        public override void Tick(StateManager states)
        {
            UIManager ui = UIManager.singleton;

            if (states.transitionFinished)
            {
                states.transitionFinished = false;
                ui.FadeInCombatCommandsUI(states.player_1);
                states.isBattleStarted = true;
            }
        }
    }
}