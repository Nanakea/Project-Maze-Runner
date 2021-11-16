using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class AIManager : MonoBehaviour
    {
        public AIStateManager aiStates;

        [Header("Stats")]
        public float totalEnemyHealth;
        public float currentEnemyHealth;
    }
}