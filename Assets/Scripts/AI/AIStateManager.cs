using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class AIStateManager : MonoBehaviour
    {
        [Space(10)]
        public GameObject activeModel;

        [Space(10)]
        public AIManager ai;

        [Header("Delta and mTransform")]
        public float delta;
        public Transform mTransform;

        [Header("Boolean")]
        public bool isDead;

        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public Collider enemyCollider;
        [HideInInspector] public Animator anim;
        [HideInInspector] public AI_AnimatorHook a_hook;

        public void Awake()
        {
            mTransform = this.transform;

            SetupAnimator();

            SetupHealth();

            SetupAIManager();
        }

        public void Start()
        {
            Init();
        }

        public void Init()
        {
            SetupRigidbody();

            SetupCollider();

            SetupAniamtorHook();
        }

        public void SetupAnimator()
        {
            if(activeModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                activeModel = anim.gameObject;
            }

            anim.applyRootMotion = false;
        }

        public void SetupHealth()
        {
            isDead = false;


        }

        public void SetupAIManager()
        {
            ai = GetComponent<AIManager>();
            ai.aiStates = this;
        }

        public void SetupRigidbody()
        {
            rb = GetComponent<Rigidbody>();
            rb.angularDrag = 999;
            rb.drag = 4;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        public void SetupCollider()
        {
            enemyCollider = GetComponent<Collider>();
        }

        public void SetupAniamtorHook()
        {
            a_hook = activeModel.GetComponent<AI_AnimatorHook>();
            if(a_hook == null)
            {
                a_hook = activeModel.AddComponent<AI_AnimatorHook>();
            }


        }
    }
}