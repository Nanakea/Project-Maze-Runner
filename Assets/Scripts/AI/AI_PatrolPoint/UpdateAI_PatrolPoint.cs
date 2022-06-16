using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class UpdateAI_PatrolPoint : MonoBehaviour
    {
        public AI_PatrolPointManager patrolPointManager;
        public int linkedPatrolPointListId;
        public int linkedPatrolPointId;

        private void Awake()
        {
            AI_PatrolPoint newAIPatrolPoint = new AI_PatrolPoint(linkedPatrolPointListId, linkedPatrolPointId, transform.position);

            // FIND THE LINK PATROL POINT FROM A MANAGER
            patrolPointManager.onScenePatrolPoints.Add(newAIPatrolPoint);
        }
    }
}