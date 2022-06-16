using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class UITransitionHook : MonoBehaviour
    {
        LevelSwitcherManager levelSwitcherManager;

        private void Awake()
        {
            levelSwitcherManager = GetComponent<LevelSwitcherManager>();
        }

        public void UITransitionMoveIn()
        {
            levelSwitcherManager.switchStateCamera.enabled = false;
            levelSwitcherManager.switchStateBattleCamera.enabled = true;
            levelSwitcherManager.switchStateCamera = null;
        }

        public void UITransitionMoveOut()
        {
            levelSwitcherManager.switchStateCamera.enabled = true;
            levelSwitcherManager.switchStateBattleCamera.enabled = false;
            levelSwitcherManager.switchStateCamera = null;
        }

        public void PairUITransitionMoveIn()
        {
            for (int i = 0; i < levelSwitcherManager.pairSwitchStateBattleCamera.Count; i++)
            {
                levelSwitcherManager.pairSwitchStateBattleCamera[i].enabled = true;
            }

            for (int i = 0; i < levelSwitcherManager.pairSwitchStateCamera.Count; i++)
            {
                levelSwitcherManager.pairSwitchStateCamera[i].enabled = false;
            }
        }

        public void PairUITransitionMoveOut()
        {
            for (int i = 0; i < levelSwitcherManager.pairSwitchStateBattleCamera.Count; i++)
            {
                levelSwitcherManager.pairSwitchStateBattleCamera[i].enabled = false;
            }

            for (int i = 0; i < levelSwitcherManager.pairSwitchStateCamera.Count; i++)
            {
                levelSwitcherManager.pairSwitchStateCamera[i].enabled = true;
            }
        }

        public void SetTransitionFinishedStatusToTrue()
        {
            levelSwitcherManager.player_1States.transitionFinished = true;
            levelSwitcherManager.player_2States.transitionFinished = true;
            levelSwitcherManager.player_1States.inp.stopPlayer1_UpdateInput = false;
            levelSwitcherManager.player_2States.inp.stopPlayer2_UpdateInput = false;
        }
    }
}