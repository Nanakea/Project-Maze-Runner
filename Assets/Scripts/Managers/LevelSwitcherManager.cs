using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class LevelSwitcherManager : MonoBehaviour
    {
        [HideInInspector] public Animator anim;

        [ReadOnlyInspector] public Camera switchStateCamera;
        [ReadOnlyInspector] public Camera switchStateBattleCamera;

        [ReadOnlyInspector] public List<Camera> pairSwitchStateCamera;
        [ReadOnlyInspector] public List<Camera> pairSwitchStateBattleCamera;

        [ReadOnlyInspector] public StateManager player_1States;
        [ReadOnlyInspector] public StateManager player_2States;

        public static LevelSwitcherManager singleton;
        private void Awake()
        {
            if (singleton == null)
                singleton = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
        }
    }
}