using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class MiniGame : MonoBehaviour
    {
        //[Header("Mini Game Type")]
        //public MiniGameTypeEnum gameType;
        
        [HideInInspector] public Result result;

        public abstract void Tick(StateManager states);
    }

    public enum MiniGameTypeEnum
    {
        Scramble,
        Others
    }
}