using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandler : MonoBehaviour
    {
        [Header("Delta")]
        public float delta;
        public float fixedDelta;

        [Header("Inputs")]
        public float mouseSensitivity = 3.5f;
        public float cameraPitch;
        public float horizontal;
        public float vertical;
        public float mouseX;
        public float mouseY;

        public Vector2 mousePosition;

        public bool mouse0;
        public bool mouse1;
        public bool mouse2;
        

        [Header("Rotate Stats")]
        public bool startCharacterRotate;
        public float rotSpeed = 7;
        public Vector3 desiredCharacterEulerAngles;

        [HideInInspector] public CameraController camController;
        StateManager states;
        UIManager ui;

        // Start is called before the first frame update
        void Start()
        {
            states = StateManager.singleton;
            camController = CameraController.singleton;
            ui = UIManager.singleton;

            states.inp = this;
            camController.inp = this;

            states.Init();
            camController.Init();

            ui.Init();
        }

        // Update is called once per frame
        private void Update()
        {
            delta = Time.deltaTime;

            GetInput();
            UpdateStates();

            RotateCharacter();

            states.Tick(delta);
            camController.Tick(delta);
            ui.Tick(this);
        }

        private void FixedUpdate()
        {
            fixedDelta = Time.fixedDeltaTime;

            states.FixedTick(fixedDelta);
        }

        private void GetInput()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            mousePosition = Input.mousePosition;
            mouse0 = Input.GetButton("Fire0");
            mouse1 = Input.GetButton("Fire1");
            mouse2 = Input.GetButton("Fire2");
        }

        private void UpdateStates()
        {
            
        }

        public void StartCharacterRotationLeft()
        {
            startCharacterRotate = true;
            desiredCharacterEulerAngles = -states.mTransform.right;
        }

        public void StartCharacterRotationRight()
        {
            startCharacterRotate = true;
            desiredCharacterEulerAngles = states.mTransform.right;
        }

        public void RotateCharacter()
        {
            if (startCharacterRotate)
            {
                Quaternion lookRotation = Quaternion.LookRotation(desiredCharacterEulerAngles);

                states.mTransform.rotation = Quaternion.Slerp(states.mTransform.rotation, lookRotation, delta * rotSpeed);

                if(Vector3.Angle(states.mTransform.eulerAngles, desiredCharacterEulerAngles) <= 5)
                {
                    startCharacterRotate = false;
                }
            }
        }
    }
}