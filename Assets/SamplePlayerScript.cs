using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class SamplePlayerScript : MonoBehaviour
    {
        public int id;

        public string playerName;
        public string backStory;
        public float health;
        public float damage;

        public float weapon1Damge, weapon2Damage;

        public string showName;
        public int shoeSize;
        public string showType;

        // Start is called before the first frame update
        void Start()
        {
            health = 50;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}