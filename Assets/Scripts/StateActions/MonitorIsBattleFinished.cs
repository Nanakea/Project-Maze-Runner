using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "State Actions/MonitorIsBattleFinished")]
    public class MonitorIsBattleFinished : StateAction
    {
        public override void Tick(StateManager states)
        {
            if (states.isBattleFinished)
            {
                if (states.enter)
                {
                    states.isBattleFinished = false;

                    if (states.playerEnemyStates != null)
                    {
                        if (states.isDead)
                        {
                            UIManager.singleton.FadeOutDefeatedUI(states.player_1);
                            UIManager.singleton.FadeOutVictoryUI(states.playerEnemyStates.player_1);
                            if (states.player_1)
                            {
                                states.inp.stopPlayer1_UpdateInput = true;
                            }
                            else
                            {
                                states.inp.stopPlayer2_UpdateInput = true;
                            }
                        }
                        else
                        {
                            UIManager.singleton.FadeOutVictoryUI(states.player_1);
                            UIManager.singleton.FadeOutDefeatedUI(states.playerEnemyStates.player_1);
                        }
                    }
                    else
                    {
                        UIManager.singleton.FadeOutVictoryUI(states.player_1);
                    }

                    states.ResetToIdleState();
                }
            }
        }
    }
}