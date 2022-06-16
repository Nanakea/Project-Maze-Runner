using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace SA
{
    public class StateManager : MonoBehaviour
    {
        [Header("StateManagerVariable")]
        public StateManagerVariables stateManagerVariable;

        [Header("Current Enemy State")]
        public AIStateManager currentEnemyStates;
        public StateManager playerEnemyStates;

        [Header("Current State")]
        public State currentState;

        [Header("Movements")]
        [ReadOnlyInspector] public float delta;
        [ReadOnlyInspector] public float moveAmount;
        [ReadOnlyInspector] public Vector2 moveDir;

        [Header("Inputs")]
        [ReadOnlyInspector] public float horizontal;
        [ReadOnlyInspector] public float vertical;
        [ReadOnlyInspector] public bool enter;

        [ReadOnlyInspector] public Vector2 mousePosition;
        [ReadOnlyInspector] public float mouseX;
        [ReadOnlyInspector] public float mouseY;

        [ReadOnlyInspector] public bool mouse0;
        [ReadOnlyInspector] public bool mouse1;
        [ReadOnlyInspector] public bool mouse2;

        [Header("Bools")]
        public bool player_1;
        [ReadOnlyInspector] public bool isDiscovered;
        [ReadOnlyInspector] public bool isDiscoveredPlayerEnemy;
        [ReadOnlyInspector] public bool isCaughtPlayerEnemy;
        [ReadOnlyInspector] public bool isFacedEnemy;
        [ReadOnlyInspector] public bool isBattleStarted;
        [ReadOnlyInspector] public bool isBattleFinished;
        [ReadOnlyInspector] public bool isDead;
        [ReadOnlyInspector] public bool transitionFinished;
        [ReadOnlyInspector] public bool miniGameStarted;
        [ReadOnlyInspector] public bool isAttacked;

        [Header("Current Mini Game Score")]
        public float currentScore;

        [Header("Refs")]
        [HideInInspector] public InputHandler inp;
        [HideInInspector] public Transform mTransform;
        [HideInInspector] public CharacterController controllerComponent;
        [HideInInspector] public CameraController camController;
        [HideInInspector] public MiniGame currentMiniGame;
        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public Collider col;
        public Collider[] playerCols = new Collider[1];

        [Header("Player Stats")]
        public PlayerStatsManager playerStatsManager;

        [HideInInspector] public readonly Vector3 vector3Zero = new Vector3(0, 0, 0);
        [HideInInspector] public Vector2 vector2Zero = new Vector2(0, 0);
        [HideInInspector] public Vector2 vector2Up = new Vector2(0, 1);
        [ReadOnlyInspector] public Vector3 destinationPosition;

        [Header("Color")]
        [ReadOnlyInspector] public Renderer render;
        public Color idleStateColor;
        public Color facingPlayerStateColor;
        public Color isCaughtColor;

        [Header("Cam Culling LayerMask")]
        public LayerMask layermaskForEnemy;
        public LayerMask layermaskForDiscovered;
        public LayerMask layermaskForBeingDiscoverd;

        public void Init()
        {
            SetUpGameObject();

            SetupCharacterController();

            SetupCameraController();

            SetupStateManagerVariable();

            SetupUIManager();

            SetupAgent();

            SetupRender();

            SetupCollider();

            SetupLevelSwitcherManager();

            SubscribledToBattleEvent();

            playerStatsManager.Init();
        }

        public void Tick(float delta)
        {
            this.delta = delta;

            if (!miniGameStarted)
            {
                if (currentState != null)
                {
                    currentState.Tick(this);

                    if (currentState.forwardTransitions != null)
                    {
                        currentState.CheckTransitions(this);
                    }
                }
            }
            else
            {
                currentMiniGame.Tick(this);
            }

            UIManager.singleton.UpdatePlayerHealthStats(this);
        }

        public void FixedTick(float fixedDelta)
        {
            camController.Tick(delta);

            if (currentState != null)
                currentState.FixedTick(this);
        }
        
        public void ResetToIdleState()
        {
            UIManager.singleton.FadeOutScrambleGameBackgroundUI(player_1);

            if (currentEnemyStates != null)
            {
                currentEnemyStates.mTransform.position = currentEnemyStates.posBeforeBattle;
                currentEnemyStates.mTransform.eulerAngles = currentEnemyStates.eulerBeforeBattle;
                currentEnemyStates.ai.agent.enabled = true;

                currentEnemyStates.currentPlayerStates = null;
                currentEnemyStates.currentState = StateDataManager.singleton.enemyPatrolState;
                currentEnemyStates.isAggro = false;
                currentEnemyStates.isFacedPlayer = false;
                currentEnemyStates.IsPatrolInited = false;
                currentEnemyStates.aggroTransitionWaitTimer = 0;

                List<AI_PatrolPoint> currentEnemyPatrolPointList = currentEnemyStates.currentPartrolPointList.patrolPoints;
                currentEnemyStates.mTransform.position = currentEnemyPatrolPointList[currentEnemyPatrolPointList.Count - 1].pos;
                currentEnemyStates = null;

                UIManager.singleton.FadeOutCombatCommandsUI(player_1);
                UIManager.singleton.PlayUITransitionMoveOut(this);
            }

            transitionFinished = false;
            isDiscovered = false;
            isDiscoveredPlayerEnemy = false;
            isCaughtPlayerEnemy = false;
            isFacedEnemy = false;
            isBattleStarted = false;
            miniGameStarted = false;
            render.material.color = idleStateColor;
            agent.isStopped = true;
            currentState = StateDataManager.singleton.playerIdleState;

            if (playerEnemyStates != null)
            {
                if (playerEnemyStates.isDead)
                {
                    if (player_1)
                        inp.stopPlayer1_UpdateInput = true;
                    else
                        inp.stopPlayer2_UpdateInput = true;

                    UIManager.singleton.FadeInFinalWinUI(player_1);
                    playerEnemyStates.gameObject.SetActive(false);
                    playerEnemyStates = null;
                    return;
                }
                else
                {
                    UIManager.singleton.PlayPairUITransitionMoveOut(this, playerEnemyStates);
                }
            }
        }

        #region Init

        private void SetUpGameObject()
        {
            mTransform = gameObject.transform;

            if (player_1)
                gameObject.layer = 9;
            else
                gameObject.layer = 10;
        }

        private void SetupCollider()
        {
            col = GetComponent<Collider>();
        }

        private void SetupStateManagerVariable()
        {
            stateManagerVariable.value = this;
        }

        private void SetupCharacterController()
        {
            controllerComponent = GetComponent<CharacterController>();
        }

        private void SetupCameraController()
        {
            camController = GetComponentInChildren<CameraController>();
            camController.Init();
        }

        private void SetupUIManager()
        {
            if (player_1)
                UIManager.singleton.player1States = this;
            else
                UIManager.singleton.player2States = this;
        }

        public void SetupAgent()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
        }

        public void SetupRender()
        {
            render = GetComponentInChildren<Renderer>();
        }

        public void SetupLevelSwitcherManager()
        {
            if (player_1)
                LevelSwitcherManager.singleton.player_1States = this;
            else
                LevelSwitcherManager.singleton.player_2States = this;
        }

        public void SubscribledToBattleEvent()
        {
            BattleEvents.OnPlayerAttack += TakeDamage;
        }

        public void UnSubscribledBattleEvent()
        {
            BattleEvents.OnPlayerAttack -= TakeDamage;
        }

        public void TakeDamage(float damage, StateManager attackStates, bool isPlayersBattle)
        {
            if (isPlayersBattle)
            {
                if (!attackStates.isAttacked)
                {
                    Debug.Log("attackStates.playerEnemyStates.playerStatsManager._hp = " + attackStates.playerEnemyStates.playerStatsManager._hp);
                    attackStates.playerEnemyStates.playerStatsManager._hp -= damage;
                    attackStates.isAttacked = true;
                    attackStates.miniGameStarted = false;

                    if (attackStates.playerEnemyStates.playerStatsManager._hp <= 0)
                    {
                        attackStates.playerEnemyStates.playerStatsManager._hp = 0;
                        attackStates.playerEnemyStates.isDead = true;

                        UIManager.singleton.FadeOutCombatCommandsUI(attackStates.playerEnemyStates.player_1);
                    }
                    else
                    {
                        UIManager.singleton.FadeInCombatCommandsUI(attackStates.player_1);
                    }
                }
            }
        }

        #endregion
    }
}

// Point and Click Movement System:

#region Variables
//public NavMeshAgent agent;
//private const float navMeshSampleDistance = 4;
#endregion

#region Nav Agent

    /*
    public void SetupAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        destinationPosition = transform.position;
        agent.SetDestination(destinationPosition);
    }
    */

#endregion

#region OnGroundClick
/*
    public void OnGroundClick(BaseEventData data)
    {
        inp.SetStartCharacterRotate(false);

        PointerEventData pData = (PointerEventData)data;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(pData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
            destinationPosition = hit.position;
        else
            destinationPosition = pData.pointerCurrentRaycast.worldPosition;

        agent.SetDestination(destinationPosition);
        agent.isStopped = false;
    }
    */
#endregion