using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class UIManager : MonoBehaviour
    {
        public Image cursor;

        public Sprite normalCursor;
        public Sprite clickedCursor;


        [Header("Controller Stats")]
        public bool CursorVisible;


        // REFERENCES
        [HideInInspector] public static UIManager singleton;
        public InputHandler inp;

        private void Awake()
        {
            if(singleton == null)
            {
                singleton = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void Init()
        {
        }

        public void Tick(InputHandler inp)
        {
            Cursor.visible = CursorVisible;

            cursor.rectTransform.position = inp.mousePosition;

            if (inp.mouse0)
                cursor.sprite = clickedCursor;
            else
                cursor.sprite = normalCursor;
        }
    }
}