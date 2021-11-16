using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class StateManager : MonoBehaviour
    {
        [HideInInspector] public InputHandler inp;
        [HideInInspector] public Transform mTransform;
        [HideInInspector] public CharacterController controllerComponent;
        
        public float walkSpeed;
        public float gravity = -13f;

        [Header("Non Editable")]
        public float moveAmount;
        public Vector3 moveDir;

        [HideInInspector] public Vector2 vector2Zero = new Vector2(0, 0);
        [HideInInspector] public Vector2 vector2Up = new Vector2(0, 1);

        public static StateManager singleton;
        private void Awake()
        {
            if(singleton == null)
            {
                singleton = this;
            }
            else if(singleton != this)
            {
                Destroy(this);
            }
        }

        public void Init()
        {
            mTransform = gameObject.transform;

            controllerComponent = GetComponent<CharacterController>();
        }

        public void Tick(float delta)
        {
            
        }

        public void FixedTick(float fixedDelta)
        {

        }
    }
}