using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [System.Serializable]
    public class AI_PatrolPoint
    {
        public int patrolPointListId;
        public int patrolPointId;
        public Vector3 pos;

        public AI_PatrolPoint(int patrolPointListId, int patrolPointId, Vector3 pos)
        {
            this.patrolPointListId = patrolPointListId;
            this.patrolPointId = patrolPointId;
            this.pos = pos;
        }
    }
}