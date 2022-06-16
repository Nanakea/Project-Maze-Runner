using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class CameraController : MonoBehaviour
    {
        [ReadOnlyInspector]
        public Camera main_cam;
        [ReadOnlyInspector]
        public Camera battle_cam;

        Vector3 battle_cam_position;
        Vector3 battle_cam_eulers;

        [Header("Configuration")]
        [SerializeField]
        public float mouseSensitivity = 3.5f;

        [Range(0.0f, 0.5f), SerializeField]
        public float mouseSmoothTime = 0.03f;

        public void Init()
        {
            Camera[] cams = gameObject.GetComponentsInChildren<Camera>();
            for (int i = 0; i < cams.Length; i++)
            {
                if (cams[i].gameObject == this.gameObject)
                {
                    main_cam = cams[i];
                    main_cam.enabled = true;
                    main_cam.cullingMask = LayerMask.GetMask("Default");
                    main_cam.cullingMask += LayerMask.GetMask("Enemy");
                }
                else
                {
                    battle_cam = cams[i];
                    battle_cam.enabled = false;
                    battle_cam.cullingMask = LayerMask.GetMask("Default");
                    battle_cam.cullingMask += LayerMask.GetMask("Enemy");

                    battle_cam_position = battle_cam.gameObject.transform.position;
                    battle_cam_eulers = battle_cam.gameObject.transform.eulerAngles;
                }
            }
        }

        public void Tick(float delta)
        {
            battle_cam.gameObject.transform.position = battle_cam_position;
            battle_cam.gameObject.transform.eulerAngles = battle_cam_eulers;
        }
    }
}