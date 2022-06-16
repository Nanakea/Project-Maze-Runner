using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class AI_ScoreCalculation : ScriptableObject
    {
        [Header("ReturnScore")]
        public int ReturnScore;

        public abstract int ReturnScoreCalculationResult(AIManager ai);
    }
}