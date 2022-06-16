using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [System.Serializable]
    public class AI_PatrolPointList
    { 
        public int patrolPointListId;
        public List<AI_PatrolPoint> patrolPoints = new List<AI_PatrolPoint>();    // BEFORE POPUALTE IT, HAVE TO CLEAR IT FIRST

        public AI_PatrolPointList(int patrolPointListId)
        {
            this.patrolPointListId = patrolPointListId;
        }
    }
}