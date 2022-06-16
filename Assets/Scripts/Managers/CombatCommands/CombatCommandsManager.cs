using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace SA
{
    public class CombatCommandsManager : MonoBehaviour
    {
        [Header("PlayerState Variable")]
        public StateManagerVariables player1States;
        public StateManagerVariables player2States;

        [Header("Games")]
        public MiniGameTypeEnum currentGameType;

        #region Commands options maneuver system
        [Header("Options Maneuver")]
        public CombatCommand[] player1CommandRects;
        [Space(10)]
        public CombatCommand[] player2CommandRects;

        CombatCommand player1CurrentCommand;
        CombatCommand player2CurrentCommand;

        [Header("Current Pos")]
        [ReadOnlyInspector]
        public int p1_OptionsPos = 0;
        [ReadOnlyInspector]
        public int p2_OptionsPos = 0;
        
        [Header("Input Wait")]
        public float player1InputWaitRate;
        public float player2InputWaitRate;

        [ReadOnlyInspector]
        public float player1InputWaitTimer;
        [ReadOnlyInspector]
        public float player2InputWaitTimer;

        [Header("Color")]
        public Color initalColor;
        public Color pressedColor;

        #endregion

        private void Update()
        {
            if (player1States.value.isBattleStarted)
            {
                CommandsOptionManeuverPlayer1();
                player1States.value.vertical = 0;
            }

            if (player2States.value.isBattleStarted)
            {
                CommandsOptionManeuverPlayer2();
                player2States.value.vertical = 0;
            }
        }

        void CommandsOptionManeuverPlayer1()
        {
            GetInputPlayer1();

            CombatCommand newCommandRects1 = player1CommandRects[p1_OptionsPos];

            if (player1CurrentCommand == null)
            {
                player1CurrentCommand = newCommandRects1;
                player1CurrentCommand.commandsText.color = pressedColor;
            }
            else if (newCommandRects1 != player1CurrentCommand)
            {
                player1CurrentCommand.commandsText.color = initalColor;
                player1CurrentCommand = newCommandRects1;
                player1CurrentCommand.commandsText.color = pressedColor;
            }

            if (player1States.value.enter && !player1States.value.miniGameStarted && !player1States.value.isDead && !player2States.value.isDead)
            {
                player1CurrentCommand.Execute(player1States.value);
                p1_OptionsPos = 0;
            }
        }

        void GetInputPlayer1()
        {
            player1InputWaitTimer += player1States.value.delta;
            if (player1InputWaitTimer >= player1InputWaitRate && player1States.value.vertical != 0)
            {
                player1InputWaitTimer = 0;
                if (player1States.value.vertical > 0)
                {
                    p1_OptionsPos--;
                    if (p1_OptionsPos < 0)
                    {
                        p1_OptionsPos = player1CommandRects.Length - 1;
                    }
                }
                else
                {
                    p1_OptionsPos++;
                    if (p1_OptionsPos > player1CommandRects.Length - 1)
                    {
                        p1_OptionsPos = 0;
                    }
                }
            }
        }

        void CommandsOptionManeuverPlayer2()
        {
            GetInputPlayer2();

            CombatCommand newCommandRects2 = player2CommandRects[p2_OptionsPos];

            if (player2CurrentCommand == null)
            {
                player2CurrentCommand = newCommandRects2;
                player2CurrentCommand.commandsText.color = pressedColor;
            }
            else if (newCommandRects2 != player2CurrentCommand)
            {
                player2CurrentCommand.commandsText.color = initalColor;
                player2CurrentCommand = newCommandRects2;
                player2CurrentCommand.commandsText.color = pressedColor;
            }

            if (player2States.value.enter && !player2States.value.miniGameStarted && !player1States.value.isDead && !player2States.value.isDead)
            {
                player2CurrentCommand.Execute(player2States.value);
                p2_OptionsPos = 0;
            }
        }

        void GetInputPlayer2()
        {
            player2InputWaitTimer += player2States.value.delta;
            if (player2InputWaitTimer >= player2InputWaitRate && player2States.value.vertical != 0)
            {
                player2InputWaitTimer = 0;
                if (player2States.value.vertical > 0)
                {
                    p2_OptionsPos--;
                    if (p2_OptionsPos < 0)
                    {
                        p2_OptionsPos = player2CommandRects.Length - 1;
                    }
                }
                else
                {
                    p2_OptionsPos++;
                    if (p2_OptionsPos > player2CommandRects.Length - 1)
                    {
                        p2_OptionsPos = 0;
                    }
                }
            }
        }
    }

    public abstract class CombatCommand : MonoBehaviour
    {
        [ReadOnlyInspector]
        public Text commandsText;

        public abstract void Execute(StateManager states);
    }
}