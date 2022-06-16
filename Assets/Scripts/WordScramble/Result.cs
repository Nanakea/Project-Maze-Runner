using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace SA
{
    [System.Serializable]
    public class Result
    {
        public float player1TotalScore = 0;
        public float player2TotalScore = 0;

        [HideInInspector] public MiniGame currentMiniGame;
    }
}