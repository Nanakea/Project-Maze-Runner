using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class UIManager : MonoBehaviour
    {
        [Header("Rect Transform")]
        public RectTransform player1HealthRect;
        public RectTransform player2HealthRect;
        public RectTransform player1combatCommandsUIRect;
        public RectTransform player2combatCommandsUIRect;
        public RectTransform player1ScrambleGameUIRect;
        public RectTransform player2ScrambleGameUIRect;
        public RectTransform player1ScrambleGameStatsRect;
        public RectTransform player2ScrambleGameStatsRect;
        public RectTransform player1DamageDealtRect;
        public RectTransform player2DamageDealtRect;
        public RectTransform player1VictoryUIRect;
        public RectTransform player2VictoryUIRect;
        public RectTransform player1DefeatedUIRect;
        public RectTransform player2DefeatedUIRect;
        public RectTransform player1FinalWinUIRect;
        public RectTransform player2FinalWinUIRect;


        [Header("Controller Stats")]
        [SerializeField]
        public float healthbarBuffer = 0;
        
        [Header("References")]
        [HideInInspector]
        public StateManager player1States;
        [HideInInspector]
        public StateManager player2States;

        [Header("Canvas")]
        Canvas player1combatCommandsUICanvas;
        Canvas player2combatCommandsUICanvas;

        Canvas player1ScrambleGameUICanvas;
        Canvas player2ScrambleGameUICanvas;

        Canvas player1ScrambleStatsCanvas;
        Canvas player2ScrambleStatsCanvas;

        Canvas player1DamageDealtCanvas;
        Canvas player2DamageDealtCanvas;

        Canvas player1VictoryUICanvas;
        Canvas player2VictoryUICanvas;

        Canvas player1DefeatedUICanvas;
        Canvas player2DefeatedUICanvas;

        Canvas player1FinalWinUICanvas;
        Canvas player2FinalWinUICanvas;

        [Header("Slider")]
        public Slider player1HealthSlider;
        public Slider player1_HealthVisSlider;
        public Slider player2HealthSlider;
        public Slider player2_HealthVisSlider;
        public Slider e_healthSlider;
        public Slider e_healthVisSlider;
        public float lerpSpeed = 2;

        [Header("Text")]
        public Text player1TimeLimitText;
        public Text player2TimeLimitText;

        public Text player1TotalScoreText;
        public Text player2TotalScoreText;

        public Text player1DamageDealtNumberText;
        public Text player2DamageDealtNumberText;

        public Text player1TriesText;
        public Text player2TriesText;

        public Color timeLimitColor;

        public static UIManager singleton;
        private void Awake()
        {
            if(singleton == null)
            {
                singleton = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void Init()
        {
            InitPlayerHealthSlider();
            InitCombatCommandsUI();
            InitScrambleGameUI();
            InitScrambleGameStatsUI();
            InitScrambleGameText();
            InitDamageDealtUI();
            InitVictoryUI();
            InitDefeatedUI();
            InitFinalWinUI();
        }

        #region Init

        void InitPlayerHealthSlider()
        {
            player1HealthSlider.maxValue = player1States.playerStatsManager.hp;
            player1_HealthVisSlider.maxValue = player1States.playerStatsManager.hp;
            
            player2HealthSlider.maxValue = player2States.playerStatsManager.hp;
            player2_HealthVisSlider.maxValue = player2States.playerStatsManager.hp;
        }

        void InitCombatCommandsUI()
        {
            player1combatCommandsUICanvas = player1combatCommandsUIRect.GetComponent<Canvas>();
            player1combatCommandsUICanvas.enabled = false;
            LeanTween.alpha(player1combatCommandsUIRect, 0, 0.1f);

            player2combatCommandsUICanvas = player2combatCommandsUIRect.GetComponent<Canvas>();
            player2combatCommandsUICanvas.enabled = false;
            LeanTween.alpha(player2combatCommandsUIRect, 0, 0.1f);
        }

        void InitScrambleGameUI()
        {
            player1ScrambleGameUICanvas = player1ScrambleGameUIRect.GetComponent<Canvas>();
            player1ScrambleGameUICanvas.enabled = false;
            LeanTween.alpha(player1ScrambleGameUIRect, 0, 0.1f);

            player2ScrambleGameUICanvas = player2ScrambleGameUIRect.GetComponent<Canvas>();
            player2ScrambleGameUICanvas.enabled = false;
            LeanTween.alpha(player2ScrambleGameUIRect, 0, 0.1f);
        }

        void InitScrambleGameStatsUI()
        {
            player1ScrambleStatsCanvas = player1ScrambleGameStatsRect.GetComponent<Canvas>();
            player1ScrambleStatsCanvas.enabled = false;
            LeanTween.alpha(player1ScrambleGameStatsRect, 0, 0.1f);

            player2ScrambleStatsCanvas = player2ScrambleGameStatsRect.GetComponent<Canvas>();
            player2ScrambleStatsCanvas.enabled = false;
            LeanTween.alpha(player2ScrambleGameStatsRect, 0, 0.1f);
        }

        void InitScrambleGameText()
        {
            player1TimeLimitText.text = "";
            player2TimeLimitText.text = "";

            player1TotalScoreText.text = "";
            player2TotalScoreText.text = "";
        }

        void InitDamageDealtUI()
        {
            player1DamageDealtCanvas = player1DamageDealtRect.GetComponent<Canvas>();
            player1DamageDealtCanvas.enabled = false;
            LeanTween.alpha(player1DamageDealtRect, 0, 0.1f);

            player2DamageDealtCanvas = player2DamageDealtRect.GetComponent<Canvas>();
            player2DamageDealtCanvas.enabled = false;
            LeanTween.alpha(player2DamageDealtRect, 0, 0.1f);
        }

        public void InitEnemyHealthSlider(AIStateManager aiStates)
        {
            e_healthSlider.maxValue = aiStates.data.maxHealth;
            e_healthVisSlider.maxValue = aiStates.data.maxHealth;
        }

        void InitVictoryUI()
        {
            player1VictoryUICanvas = player1VictoryUIRect.GetComponent<Canvas>();
            player1VictoryUICanvas.enabled = false;
            LeanTween.alpha(player1VictoryUIRect, 0, 0.1f);

            player2VictoryUICanvas = player2VictoryUIRect.GetComponent<Canvas>();
            player2VictoryUICanvas.enabled = false;
            LeanTween.alpha(player2VictoryUIRect, 0, 0.1f);
        }

        void InitDefeatedUI()
        {
            player1DefeatedUICanvas = player1DefeatedUIRect.GetComponent<Canvas>();
            player1DefeatedUICanvas.enabled = false;
            LeanTween.alpha(player1DefeatedUIRect, 0, 0.1f);

            player2DefeatedUICanvas = player2DefeatedUIRect.GetComponent<Canvas>();
            player2DefeatedUICanvas.enabled = false;
            LeanTween.alpha(player2DefeatedUIRect, 0, 0.1f);
        }

        void InitFinalWinUI()
        {
            player1FinalWinUICanvas = player1FinalWinUIRect.GetComponent<Canvas>();
            player1FinalWinUICanvas.enabled = false;
            LeanTween.alpha(player1FinalWinUIRect, 0, 0.1f);

            player2FinalWinUICanvas = player2FinalWinUIRect.GetComponent<Canvas>();
            player2FinalWinUICanvas.enabled = false;
            LeanTween.alpha(player2FinalWinUIRect, 0, 0.1f);
        }

        #endregion

        public void Tick()
        {
        }
        
        public void FadeInCombatCommandsUI(bool isPlayer_1)
        {
            if (isPlayer_1)
            {
                if (!player1combatCommandsUICanvas.enabled)
                    player1combatCommandsUICanvas.enabled = true;

                LeanTween.alpha(player1combatCommandsUIRect, 1, 0.3f);
            }
            else
            {
                if (!player2combatCommandsUICanvas.enabled)
                    player2combatCommandsUICanvas.enabled = true;

                LeanTween.alpha(player2combatCommandsUIRect, 1, 0.3f);
            }
        }

        public void FadeOutCombatCommandsUI(bool isPlayer_1)
        {
            if (isPlayer_1)
            {
                if (player1combatCommandsUICanvas.enabled)
                    player1combatCommandsUICanvas.enabled = false;

                LeanTween.alpha(player1combatCommandsUIRect, 0, 0.1f);
            }
            else
            {
                if (player2combatCommandsUICanvas.enabled)
                    player2combatCommandsUICanvas.enabled = false;

                LeanTween.alpha(player2combatCommandsUIRect, 0, 0.1f);
            }
        }

        public void FadeInScrambleGameBackgroundUI(StateManager states)
        {
            if (states.player_1)
            {
                if (!player1ScrambleGameUICanvas.enabled)
                    player1ScrambleGameUICanvas.enabled = true;

                LeanTween.alpha(player1ScrambleGameUIRect, 1, 0.3f);

                player1TotalScoreText.text = states.currentScore.ToString();
            }
            else
            {
                if (!player2ScrambleGameUICanvas.enabled)
                    player2ScrambleGameUICanvas.enabled = true;

                LeanTween.alpha(player2ScrambleGameUIRect, 1, 0.3f);

                player2TotalScoreText.text = states.currentScore.ToString();
            }
        }

        public void FadeOutScrambleGameBackgroundUI(bool isPlayer_1)
        {
            if (isPlayer_1)
            {
                if (player1ScrambleGameUICanvas.enabled)
                    player1ScrambleGameUICanvas.enabled = false;

                LeanTween.alpha(player1ScrambleGameUIRect, 0, 0.1f);
            }
            else
            {
                if (player2ScrambleGameUICanvas.enabled)
                    player2ScrambleGameUICanvas.enabled = false;

                LeanTween.alpha(player2ScrambleGameUIRect, 0, 0.1f);
            }
        }

        public void FadeInScrambleGameStatsUI(bool player_1)
        {
            if (player_1)
            {
                if (!player1ScrambleStatsCanvas.enabled)
                    player1ScrambleStatsCanvas.enabled = true;

                LeanTween.alpha(player1ScrambleGameStatsRect, 1, 0.3f);
            }
            else
            {
                if (!player2ScrambleStatsCanvas.enabled)
                    player2ScrambleStatsCanvas.enabled = true;

                LeanTween.alpha(player2ScrambleGameStatsRect, 1, 0.3f);
            }
        }

        public void FadeOutScrambleGameStatsUI(bool player_1)
        {
            if (player_1)
            {
                if (player1ScrambleStatsCanvas.enabled)
                    player1ScrambleStatsCanvas.enabled = false;

                LeanTween.alpha(player1ScrambleGameStatsRect, 0, 0.1f);
            }
            else
            {
                if (player2ScrambleStatsCanvas.enabled)
                    player2ScrambleStatsCanvas.enabled = false;

                LeanTween.alpha(player2ScrambleGameStatsRect, 0, 0.1f);
            }
        }

        public void FadeInDamageDealtUI(bool player_1, bool isPlayersBattle)
        {
            if (!isPlayersBattle)
            {
                if (player_1)
                {
                    if (!player1DamageDealtCanvas.enabled)
                        player1DamageDealtCanvas.enabled = true;

                    LeanTween.alpha(player1DamageDealtRect, 1, 0.4f);
                }
                else
                {
                    if (!player2DamageDealtCanvas.enabled)
                        player2DamageDealtCanvas.enabled = true;

                    LeanTween.alpha(player2DamageDealtRect, 1, 0.4f);
                }
            }
            else
            {
                if (player_1)
                {
                    if (!player2DamageDealtCanvas.enabled)
                        player2DamageDealtCanvas.enabled = true;

                    LeanTween.alpha(player2DamageDealtRect, 1, 0.4f);
                }
                else
                {
                    if (!player1DamageDealtCanvas.enabled)
                        player1DamageDealtCanvas.enabled = true;

                    LeanTween.alpha(player1DamageDealtRect, 1, 0.4f);
                }
            }
        }

        public void FadeOutDamageDealtUI(bool player_1, bool isPlayersBattle)
        {
            if (!isPlayersBattle)
            {
                if (player_1)
                {
                    if (player1DamageDealtCanvas.enabled)
                        player1DamageDealtCanvas.enabled = false;

                    LeanTween.alpha(player1DamageDealtRect, 0, 0.5f);
                }
                else
                {
                    if (player2DamageDealtCanvas.enabled)
                        player2DamageDealtCanvas.enabled = false;

                    LeanTween.alpha(player2DamageDealtRect, 0, 0.5f);
                }
            }
            else
            {
                if (player_1)
                {
                    Debug.Log("player_1");
                    if (player1DamageDealtCanvas.enabled)
                        player1DamageDealtCanvas.enabled = false;

                    LeanTween.alpha(player1DamageDealtRect, 0, 0.2f);
                }
                else
                {
                    Debug.Log("player_2");
                    if (player2DamageDealtCanvas.enabled)
                        player2DamageDealtCanvas.enabled = false;

                    LeanTween.alpha(player2DamageDealtRect, 0, 0.2f);
                }
            }
        }

        public void FadeInVictoryUI(bool player_1)
        {
            if (player_1)
            {
                if (!player1VictoryUICanvas.enabled)
                    player1VictoryUICanvas.enabled = true;

                LeanTween.alpha(player1VictoryUIRect, 1, 0.4f);
            }
            else
            {
                if (!player2VictoryUICanvas.enabled)
                    player2VictoryUICanvas.enabled = true;

                LeanTween.alpha(player2VictoryUIRect, 1, 0.4f);
            }
        }

        public void FadeOutVictoryUI(bool player_1)
        {
            if (player_1)
            {
                if (player1VictoryUICanvas.enabled)
                    player1VictoryUICanvas.enabled = false;

                LeanTween.alpha(player1VictoryUIRect, 0, 0.5f);
            }
            else
            {
                if (player2VictoryUICanvas.enabled)
                    player2VictoryUICanvas.enabled = false;

                LeanTween.alpha(player2VictoryUIRect, 0, 0.5f);
            }
        }

        public void FadeInDefeatedUI(bool player_1)
        {
            if (player_1)
            {
                if (!player1DefeatedUICanvas.enabled)
                    player1DefeatedUICanvas.enabled = true;

                LeanTween.alpha(player1DefeatedUIRect, 1, 0.4f);
            }
            else
            {
                if (!player2DefeatedUICanvas.enabled)
                    player2DefeatedUICanvas.enabled = true;

                LeanTween.alpha(player2DefeatedUIRect, 1, 0.4f);
            }
        }

        public void FadeOutDefeatedUI(bool player_1)
        {
            if (player_1)
            {
                if (player1DefeatedUICanvas.enabled)
                    player1DefeatedUICanvas.enabled = false;

                LeanTween.alpha(player1DefeatedUIRect, 0, 0.5f);
            }
            else
            {
                if (player2DefeatedUICanvas.enabled)
                    player2DefeatedUICanvas.enabled = false;

                LeanTween.alpha(player2DefeatedUIRect, 0, 0.5f);
            }
        }

        public void FadeInFinalWinUI(bool player_1)
        {
            if (player_1)
            {
                if (!player1FinalWinUICanvas.enabled)
                    player1FinalWinUICanvas.enabled = true;

                LeanTween.alpha(player1FinalWinUIRect, 1, 0.4f);

                player1HealthRect.gameObject.SetActive(false);
                player2HealthRect.gameObject.SetActive(false);
            }
            else
            {
                if (!player2FinalWinUICanvas.enabled)
                    player2FinalWinUICanvas.enabled = true;

                LeanTween.alpha(player2FinalWinUIRect, 1, 0.4f);

                player1HealthRect.gameObject.SetActive(false);
                player2HealthRect.gameObject.SetActive(false);
            }
        }

        public void PlayUITransitionMoveIn(StateManager states)
        {
            LevelSwitcherManager lvSwitcher = LevelSwitcherManager.singleton;

            lvSwitcher.switchStateCamera = states.camController.main_cam;
            lvSwitcher.switchStateBattleCamera = states.camController.battle_cam;
            states.camController.battle_cam.cullingMask = states.layermaskForEnemy;
            
            states.currentEnemyStates.ai.agent.enabled = false;
            states.currentEnemyStates.posBeforeBattle = states.currentEnemyStates.mTransform.position;
            states.currentEnemyStates.eulerBeforeBattle = states.currentEnemyStates.mTransform.eulerAngles;

            states.currentEnemyStates.mTransform.position = states.currentEnemyStates.posInBattle;
            states.currentEnemyStates.mTransform.eulerAngles = states.currentEnemyStates.eulerInBattle;

            lvSwitcher.anim.Play("transition_move_in");
        }

        public void PlayUITransitionMoveOut(StateManager states)
        {
            LevelSwitcherManager lvSwitcher = LevelSwitcherManager.singleton;

            lvSwitcher.switchStateCamera = states.camController.main_cam;
            lvSwitcher.switchStateBattleCamera = states.camController.battle_cam;
            lvSwitcher.anim.Play("transition_move_out");
        }

        public void PlayPairUITransitionMoveIn(StateManager states_1, StateManager states_2)
        {
            LevelSwitcherManager lvSwitcher = LevelSwitcherManager.singleton;

            lvSwitcher.pairSwitchStateCamera.Clear();
            lvSwitcher.pairSwitchStateCamera.Add(states_1.camController.main_cam);
            lvSwitcher.pairSwitchStateCamera.Add(states_2.camController.main_cam);

            lvSwitcher.pairSwitchStateBattleCamera.Clear();
            lvSwitcher.pairSwitchStateBattleCamera.Add(states_1.camController.battle_cam);
            lvSwitcher.pairSwitchStateBattleCamera.Add(states_2.camController.battle_cam);

            if (states_1.isDiscoveredPlayerEnemy)
            {
                states_1.camController.battle_cam.cullingMask = states_1.layermaskForBeingDiscoverd;
            }
            else
            {
                states_1.camController.battle_cam.cullingMask = states_1.layermaskForDiscovered;
            }

            if (states_2.isDiscoveredPlayerEnemy)
            {
                states_2.camController.battle_cam.cullingMask = states_2.layermaskForBeingDiscoverd;
            }
            else
            {
                states_2.camController.battle_cam.cullingMask = states_2.layermaskForDiscovered;
            }
            
            lvSwitcher.anim.Play("transition_move_in_2");
        }

        public void PlayPairUITransitionMoveOut(StateManager states_1, StateManager states_2)
        {
            LevelSwitcherManager lvSwitcher = LevelSwitcherManager.singleton;

            lvSwitcher.pairSwitchStateCamera.Clear();
            lvSwitcher.pairSwitchStateCamera.Add(states_1.camController.main_cam);
            lvSwitcher.pairSwitchStateCamera.Add(states_2.camController.main_cam);

            lvSwitcher.pairSwitchStateBattleCamera.Clear();
            lvSwitcher.pairSwitchStateBattleCamera.Add(states_1.camController.battle_cam);
            lvSwitcher.pairSwitchStateBattleCamera.Add(states_2.camController.battle_cam);

            lvSwitcher.anim.Play("transition_move_out_2");
        }

        public void UpdateDamageDealtText(float totalScore, bool isPlayer_1, bool isPlayersBattle, ScrambleMiniGame miniGame)
        {
            if (!isPlayersBattle)
            {
                if (isPlayer_1)
                {
                    var damage = miniGame.player1CorrectPercent * miniGame.maxDamage; //max damage is when player has 100% correct words
                    player1DamageDealtNumberText.text = damage.ToString();
                    BattleEvents.RaiseOnPlayerAttack(damage, player1States, false);
                }
                else
                {
                    var damage = miniGame.player2CorrectPercent * miniGame.maxDamage; //max damage is when player has 100% correct words
                    player2DamageDealtNumberText.text = damage.ToString();
                    BattleEvents.RaiseOnPlayerAttack(damage, player2States, false);
                }
            }
            else
            {
                if (isPlayer_1)
                {
                    player1States.isAttacked = false;
                    var damage = miniGame.player1CorrectPercent * miniGame.maxDamage; //max damage is when player has 100% correct words

                    player2DamageDealtNumberText.text = damage.ToString();
                    Debug.Log("Damage = " + damage);
                    BattleEvents.RaiseOnPlayerAttack(damage, player1States, true);
                }
                else
                {
                    player2States.isAttacked = false;
                    var damage = miniGame.player2CorrectPercent * miniGame.maxDamage; //max damage is when player has 100% correct words
                    player1DamageDealtNumberText.text = damage.ToString();
                    Debug.Log("Damage = " + damage);
                    BattleEvents.RaiseOnPlayerAttack(damage, player2States, true);
                }
            }
        }

        public void UpdateTimeLimitText(bool isPlayer_1, float timeLimit)
        {
            int intLimit = Mathf.RoundToInt(timeLimit);

            if (isPlayer_1)
            {
                if (intLimit <= 4)
                {
                    player1TimeLimitText.color = timeLimitColor;
                }
                else
                {
                    player1TimeLimitText.color = Color.white;
                }
                player1TimeLimitText.text = intLimit.ToString();
            }
            else
            {
                if (intLimit <= 4)
                {
                    player2TimeLimitText.color = timeLimitColor;
                }
                else
                {
                    player2TimeLimitText.color = Color.white;
                }
                player2TimeLimitText.text = intLimit.ToString();
            }
        }

        public void UpdateTotalScoreText(bool isPlayer_1, float totalScore)
        {
            if (isPlayer_1)
            {
                player1TotalScoreText.text = totalScore.ToString();
            }
            else
            {
                player2TotalScoreText.text = totalScore.ToString();
            }
        }

        public void UpdateTriesText(bool isPlayer_1, int tryNum)
        {
            if (isPlayer_1)
            {
                player1TriesText.text = tryNum.ToString();
            }
            else
            {
                player2TriesText.text = tryNum.ToString();
            }
        }

        public void UpdateEnemyHealthStats(AIStateManager aiState)
        {
            if (e_healthVisSlider.value != aiState.currentHealth)
            {
                e_healthSlider.value = aiState.currentHealth;
                e_healthVisSlider.value = Mathf.Lerp(e_healthVisSlider.value, aiState.currentHealth, Time.deltaTime);
                if ((e_healthVisSlider.value - aiState.currentHealth) <= 0.1f)
                {
                    FadeOutDamageDealtUI(aiState.currentPlayerStates.player_1, false);
                    e_healthVisSlider.value = aiState.currentHealth;

                    if (aiState.isDead)
                    {
                        if (aiState.currentPlayerStates.player_1)
                        {
                            FadeInVictoryUI(true);
                        }
                        else
                        {
                            FadeInVictoryUI(false);
                        }

                        aiState.gameObject.SetActive(false);
                    }
                }
            }
        }

        public void UpdatePlayerHealthStats(StateManager states)
        {
            if (states.player_1)
            {
                if (player1_HealthVisSlider.value != states.playerStatsManager._hp)
                {
                    player1HealthSlider.value = states.playerStatsManager._hp;
                    player1_HealthVisSlider.value = Mathf.Lerp(player1_HealthVisSlider.value, states.playerStatsManager._hp, Time.deltaTime);
                    if ((player1_HealthVisSlider.value - states.playerStatsManager._hp) <= 0.1f)
                    {
                        FadeOutDamageDealtUI(true, ScrambleMiniGame.singleton.isPlayersBattle);
                        player1_HealthVisSlider.value = states.playerStatsManager._hp;

                        if (states.isDead)
                        {
                            FadeInVictoryUI(false);
                            FadeInDefeatedUI(true);
                            states.isBattleFinished = true;
                            states.playerEnemyStates.isBattleFinished = true;
                        }
                    }
                }
            }
            else
            {
                if (player2_HealthVisSlider.value != states.playerStatsManager._hp)
                {
                    player2HealthSlider.value = states.playerStatsManager._hp;
                    player2_HealthVisSlider.value = Mathf.Lerp(player2_HealthVisSlider.value, states.playerStatsManager._hp, Time.deltaTime);
                    if ((player2_HealthVisSlider.value - states.playerStatsManager._hp) <= 0.1f)
                    {
                        FadeOutDamageDealtUI(false, ScrambleMiniGame.singleton.isPlayersBattle);
                        player2_HealthVisSlider.value = states.playerStatsManager._hp;

                        if (states.isDead)
                        {
                            FadeInVictoryUI(true);
                            FadeInDefeatedUI(false);
                            states.isBattleFinished = true;
                            states.playerEnemyStates.isBattleFinished = true;
                        }
                    }
                }
            }
        }
    }
}

// Point and Click Movement System:

#region Public
/*
    public void FadeOutRotationUI()
    {
        LeanTween.alpha(arrow_roundedLeftRect, 0, 0.3f);
        LeanTween.alpha(arrow_roundedRightRect, 0, 0.3f);
    }
    */

/*
public void FadeInRotationUI()
{
    LeanTween.alpha(arrow_roundedLeftRect, 1, 0.3f);
    LeanTween.alpha(arrow_roundedRightRect, 1, 0.3f);
}
*/
#endregion