using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "PlayerStatsManager")]
    public class PlayerStatsManager : ScriptableObject
    {
        public float hp;

        [ReadOnlyInspector] public float _hp;

        public void Init()
        {
            _hp = hp;
        }
    }
}