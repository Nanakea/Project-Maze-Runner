using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class AI_Action : ScriptableObject
    {
        [Header("Score Factors")]
        public AI_ScoreCalculation[] ScoreFactors;

        public int TotalScores;

        public abstract void Tick(AIManager ai);

        public void TotalScoreCalculation(AIManager ai)
        {
            TotalScores = 0;

            int scoreFactorsLength = ScoreFactors.Length;
            for (int i = 0; i < scoreFactorsLength; i++)
            {
                TotalScores += ScoreFactors[i].ReturnScoreCalculationResult(ai);
            }
        }
    }
}