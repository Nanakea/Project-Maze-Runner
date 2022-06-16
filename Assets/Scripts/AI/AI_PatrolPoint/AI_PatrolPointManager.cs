using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class AI_PatrolPointManager : MonoBehaviour
    {
        public List<AI_PatrolPoint> onScenePatrolPoints;
        public List<AI_PatrolPointList> onScenePatrolPointsList;

        private int onScenePatrolPointsCount;
        private int onScenePatrolPointListsCount;

        private AI_PatrolPoint currentOnScenePatrolPoint;

        private bool isInit;

        public static AI_PatrolPointManager singleton;
        private void Awake()
        {
            if (singleton == null)
                singleton = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            InitAIPatrolPointList();

            PopulatePatrolPointScriptableList();

            ReorderOnScenePatrolPointLists();

            isInit = true;
        }

        void InitAIPatrolPointList()
        {
            onScenePatrolPointsCount = onScenePatrolPoints.Count;

            onScenePatrolPointListsCount = onScenePatrolPointsList.Count;
            if(onScenePatrolPointListsCount != 0)
            {
                onScenePatrolPointsList.Clear();
            }
        }

        void PopulatePatrolPointScriptableList()
        {
            if (onScenePatrolPointsCount == 0)
                return;

            for (int i = 0; i < onScenePatrolPointsCount; i++)
            {
                currentOnScenePatrolPoint = onScenePatrolPoints[i];

                onScenePatrolPointListsCount = onScenePatrolPointsList.Count;
                if (onScenePatrolPointListsCount == 0)
                {
                    // MAKE A AI_PatrolPointList IMMIDATELY WITHOUT CHECK
                    CreateNewAI_PatrolPointScriptableList(currentOnScenePatrolPoint);

                    continue;
                }

                bool sameId = false;

                // Search for the onScenePatrolPointsList[i] that has the same list Id as current onScenePatrolPoints[i]
                for (int j = 0; j < onScenePatrolPointListsCount; j++)
                {
                    if (onScenePatrolPointsList[j].patrolPointListId == currentOnScenePatrolPoint.patrolPointListId)
                    {
                        onScenePatrolPointsList[j].patrolPoints.Add(currentOnScenePatrolPoint);
                        sameId = true;
                    }
                }

                if (sameId)
                {
                    continue;
                }

                CreateNewAI_PatrolPointScriptableList(currentOnScenePatrolPoint);
            }
        }

        void CreateNewAI_PatrolPointScriptableList(AI_PatrolPoint currentOnScenePatrolPoint)
        {
            AI_PatrolPointList newAI_PatrolPointScriptableList = new AI_PatrolPointList(currentOnScenePatrolPoint.patrolPointListId);
            newAI_PatrolPointScriptableList.patrolPoints.Add(currentOnScenePatrolPoint);
            onScenePatrolPointsList.Add(newAI_PatrolPointScriptableList);
        }

        void ReorderOnScenePatrolPointLists()
        {
            onScenePatrolPointListsCount = onScenePatrolPointsList.Count;
            for (int i = 0; i < onScenePatrolPointListsCount; i++)
            {
                AI_PatrolPointList currentPatrolPointList = onScenePatrolPointsList[i];
                int patrolPointCount = currentPatrolPointList.patrolPoints.Count;
                for (int j = 0; j < patrolPointCount - 1; j++)
                {
                    for (int k = 0; k < patrolPointCount - j - 1; k++)
                    {
                        if(currentPatrolPointList.patrolPoints[k].patrolPointId > currentPatrolPointList.patrolPoints[k + 1].patrolPointId)
                        {
                            AI_PatrolPoint tempPoint = currentPatrolPointList.patrolPoints[k];
                            currentPatrolPointList.patrolPoints[k] = currentPatrolPointList.patrolPoints[k + 1];
                            currentPatrolPointList.patrolPoints[k + 1] = tempPoint;
                        }
                    }
                }
            }
        }
    }
}