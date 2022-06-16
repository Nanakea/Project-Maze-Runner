using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{ 
    public class CharObject : MonoBehaviour
    {
        public char character;
        public Text text;
        public Image image;
        public RectTransform rectTransform;
        public int index;

        [Header("Appearance")]
        public Color normalColor;
        public Color selectedColor;

        bool isSelected = false;

        public void Init (char c)
        {
            character = c;
            text.text = c.ToString();
            gameObject.SetActive(true);
        }

        public void Select (ScrambleMiniGame scrambleMiniGame, bool isPlayer_1)
        {
            //Debug.Log("Select");
            isSelected = !isSelected;

            image.color = isSelected ? selectedColor : normalColor;
            if (isSelected)
            {
                scrambleMiniGame.Select(this, isPlayer_1);
            }
            else
            {
                scrambleMiniGame.UnSelect(isPlayer_1);
            }
        }
    }
}