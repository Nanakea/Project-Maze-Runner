using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class LevelManager : MonoBehaviour
    {
        [HideInInspector] public static LevelManager singleton;

        private void Awake()
        {
            if (singleton == null)
                singleton = this;
            else
                Destroy(this);
        }
    }
}