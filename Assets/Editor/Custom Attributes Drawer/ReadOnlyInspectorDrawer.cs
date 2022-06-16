using UnityEditor;
using UnityEngine;

/// <summary>
/// This class contain custom drawer for ReadOnly attribute.
/// </summary>

namespace SA
{
    [CustomPropertyDrawer(typeof(ReadOnlyInspectorAttribute))]
    public class ReadOnlyInspectorDrawer : PropertyDrawer
    {
        /// <summary>
        /// Unity method for drawing GUI in Editor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Disabling edit for property
            GUI.enabled = false;

            // Drawing Property
            EditorGUI.PropertyField(position, property, label);

            // Setting old GUI enabled value
            GUI.enabled = true;
        }
    }
}