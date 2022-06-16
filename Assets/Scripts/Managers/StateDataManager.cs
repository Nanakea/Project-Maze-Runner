using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class StateDataManager : MonoBehaviour
    {
        [Header("Player States")]
        public State playerIdleState;
        public State playerBattleState;
        public State playerFaceEnemyState;

        [Header("Enemy States")]
        public AI_State enemyPatrolState;
        public AI_State enemyFacePlayerState;
        public AI_State enemyAggroState;

        public static StateDataManager singleton;
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
    }
}