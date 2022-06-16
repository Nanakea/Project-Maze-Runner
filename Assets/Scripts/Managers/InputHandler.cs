using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandler : MonoBehaviour
    {
        [Header("Delta")]
        [ReadOnlyInspector] public float delta;
        [ReadOnlyInspector] public float fixedDelta;

        [Header("Player_1")]
        [SerializeField, ReadOnlyInspector]
        private float horizontal;
        [SerializeField, ReadOnlyInspector]
        private float vertical;
        [SerializeField, ReadOnlyInspector]
        private bool enter;

        [Header("Player_2")]
        [SerializeField, ReadOnlyInspector]
        private float horizontal_2;
        [SerializeField, ReadOnlyInspector]
        private float vertical_2;
        [SerializeField, ReadOnlyInspector]
        private bool enter_2;

        [Header("Public")]
        [SerializeField, ReadOnlyInspector]
        private bool tab;

        [Header("Mouse")]
        [SerializeField, ReadOnlyInspector]
        private Vector2 mousePosition;
        [SerializeField, ReadOnlyInspector]
        private float mouseX;
        [SerializeField, ReadOnlyInspector]
        private float mouseY;
        
        [SerializeField, ReadOnlyInspector]
        private bool mouse0;
        [SerializeField, ReadOnlyInspector]
        private bool mouse1;
        [SerializeField, ReadOnlyInspector]
        private bool mouse2;

        [Header("References")]
        private StateManager[] states;
        private StateManager player1States;
        private StateManager player2States;
        private UIManager uiManager;

        [Header("bools")]
        [SerializeField, ReadOnlyInspector]
        private bool isHorizontalSplit = false;
        public bool stopPlayer1_UpdateInput;
        public bool stopPlayer2_UpdateInput;

        // Start is called before the first frame update
        void Start()
        {
            states = FindObjectsOfType<StateManager>();
            for (int i = 0; i < 2; i++)
            {
                if (states[i].player_1 && player1States == null)
                {
                    player1States = states[i];
                    player1States.inp = this;
                    player1States.Init();
                }
                else
                {
                    player2States = states[i];
                    player2States.inp = this;
                    player2States.Init();
                }
            }

            uiManager = UIManager.singleton;
            uiManager.Init();
        }

        private void Update()
        {
            delta = Time.deltaTime;

            GetInput();

            UpdateStates();

            SwitchSplitScreenMode();

            player1States.Tick(delta);
            player2States.Tick(delta);

            uiManager.Tick();
        }

        private void FixedUpdate()
        {
            fixedDelta = Time.fixedDeltaTime;

            player1States.FixedTick(fixedDelta);
            player2States.FixedTick(fixedDelta);
        }

        private void GetInput()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            enter = Input.GetButtonDown("Enter");
            tab = Input.GetButtonDown("Tab");
            if (tab)
            {
                isHorizontalSplit = !isHorizontalSplit;
            }

            horizontal_2 = Input.GetAxisRaw("Horizontal_2");
            vertical_2 = Input.GetAxisRaw("Vertical_2");
            enter_2 = Input.GetButtonDown("Enter_2");

            mousePosition = Input.mousePosition;
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            mouse0 = Input.GetButton("Fire0");
            mouse1 = Input.GetButton("Fire1");
            mouse2 = Input.GetButton("Fire2");
        }

        private void UpdateStates()
        {
            if (!stopPlayer1_UpdateInput)
            {
                player1States.horizontal = horizontal;
                player1States.vertical = vertical;
                player1States.moveDir = new Vector3(0, vertical).normalized;
                player1States.moveAmount = Mathf.Clamp01(Mathf.Abs(vertical));
                player1States.enter = enter;
            }

            if (!stopPlayer2_UpdateInput)
            {
                player2States.horizontal = horizontal_2;
                player2States.vertical = vertical_2;
                player2States.moveDir = new Vector3(0, vertical_2).normalized;
                player2States.moveAmount = Mathf.Clamp01(Mathf.Abs(vertical_2));
                player2States.enter = enter_2;
            }

            player1States.mousePosition = mousePosition;
            player1States.mouseX = mouseX;
            player1States.mouseY = mouseY;
            player1States.mouse0 = mouse0;
            player1States.mouse1 = mouse1;
            player1States.mouse2 = mouse2;
        }

        private void SwitchSplitScreenMode()
        {
            if (isHorizontalSplit)
            {
                player1States.camController.main_cam.rect = new Rect(0f, .5f, 1f, .5f);
                player2States.camController.main_cam.rect = new Rect(0f, 0f, 1f, .5f);
            }
            else
            {
                player1States.camController.main_cam.rect = new Rect(0f, 0f, .5f, 1f);
                player2States.camController.main_cam.rect = new Rect(.5f, 0f, .5f, 1f);
            }
        }
    }
}


// Point and Click Movement System:

#region variables
//public bool startCharacterRotate;
//public float rotSpeed = 7;
//public Vector3 desiredCharacterEulerAngles;
#endregion

#region Turning buttons func 
/*
public void StartCharacterRotationLeft()
{
    startCharacterRotate = true;
    desiredCharacterEulerAngles = -states.mTransform.right;
}
*/

/*
public void StartCharacterRotationRight()
{
    startCharacterRotate = true;
    desiredCharacterEulerAngles = states.mTransform.right;
}
*/
#endregion

#region Tick
/*
public void RotateCharacter()
{
    if (startCharacterRotate)
    {
        Quaternion lookRotation = Quaternion.LookRotation(desiredCharacterEulerAngles);

        states.mTransform.rotation = Quaternion.Slerp(states.mTransform.rotation, lookRotation, delta * rotSpeed);

        float sampleDot = Vector3.Dot(states.mTransform.forward, desiredCharacterEulerAngles);
        if (sampleDot >= 1 || states.currentState != StateDataManager.singleton.playerIdleState)
        {
            startCharacterRotate = false;
        }
    }
}
*/

/*
public void SetStartCharacterRotate(bool isStartCharacterRotate)
{
    this.startCharacterRotate = isStartCharacterRotate;
}
*/
#endregion