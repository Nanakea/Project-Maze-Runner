using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    public class AIManager : MonoBehaviour
    {
        [Header("References")]
        public AIStateManager aiStates;
        public NavMeshAgent agent;

        [Header("Dir, Dis, Angle")]
        public Vector3 dirToPlayer;
        public float disToPlayer;
        public float angleToPlayer;

        [Header("Health")]
        public float totalEnemyHealth;
        public float currentEnemyHealth;

        [Header("Decision Timer")]
        public float decisionTimer;
        public float aggroThershold;

        [Header("AI Actions")]
        public AI_Action currentAction;
        public AI_ActionHolder currentActionHolder;

        public void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void Tick()
        {
            
        }
    }
}