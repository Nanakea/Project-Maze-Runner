using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class CameraController : MonoBehaviour
    {
        [HideInInspector] public Camera controllerCamera;
        [HideInInspector] public InputHandler inp;

        [Range(0.0f, 0.5f)] public float mouseSmoothTime = 0.03f;

        public static CameraController singleton;
        private void Awake()
        {
            if(singleton == null)
            {
                singleton = this;
            }
            else if(singleton != null)
            {
                Destroy(this);
            }
        }

        public void Init()
        {
            controllerCamera = gameObject.GetComponent<Camera>();
        }

        public void Tick(float delta)
        {
        }
    }
}