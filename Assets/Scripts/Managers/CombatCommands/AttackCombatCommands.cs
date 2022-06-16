using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class AttackCombatCommands : CombatCommand
    {
        private ScrambleMiniGame scrambleMiniGame;

        private void Start()
        {
            scrambleMiniGame = ScrambleMiniGame.singleton;
            commandsText = GetComponent<Text>();
        }

        public override void Execute(StateManager states)
        {
            if (states.currentEnemyStates != null)
            {
                if (states.player_1)
                {
                    scrambleMiniGame.player1Words = states.currentEnemyStates.targetWordList.value;
                }
                else
                {
                    scrambleMiniGame.player2Words = states.currentEnemyStates.targetWordList.value;
                }
            }
            else if (states.playerEnemyStates != null)
            {
                scrambleMiniGame.player1Words = scrambleMiniGame.allWordList[Random.Range(0, scrambleMiniGame.allWordList.Count)].value;
                scrambleMiniGame.player2Words = scrambleMiniGame.allWordList[Random.Range(0, scrambleMiniGame.allWordList.Count)].value;
            }

            StartCoroutine(scrambleMiniGame.Init(states.player_1));
            UIManager.singleton.FadeOutCombatCommandsUI(states.player_1);
            UIManager.singleton.FadeInScrambleGameBackgroundUI(states);
            UIManager.singleton.FadeInScrambleGameStatsUI(states.player_1);
            states.currentMiniGame = scrambleMiniGame;
            states.miniGameStarted = true;
        }

        void GetRandomAttackCommandGame()
        {
            /*
            int random = Random.Range(1, 101);
            if (random <= 101)
            {
                currentGameType = MiniGameTypeEnum.Scramble;
            }

            switch (currentGameType)
            {
                case MiniGameTypeEnum.Scramble:
                    UIManager.singleton.FadeInScrambleGameUI();
                    scrambleMiniGame.Init();
                    currentResult.currentMiniGame = scrambleMiniGame;
                    gameStarted = true;
                    BattleEvents.RaiseOnBattleStart();
                    break;
                case MiniGameTypeEnum.Others:
                    break;
                default:
                    break;
            }
            */
        }
    }
}