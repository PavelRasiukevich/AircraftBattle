using UnityEditor;
using UnityEngine;

namespace Managers.Data.CustomEditors
{
#if UNITY_EDITOR
    public abstract class BaseCustomEditor : EditorWindow
    {
        protected void HorizontalLine(Color color)
        {
            GUIStyle horizontalLine;
            horizontalLine = new GUIStyle();
            horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
            horizontalLine.margin = new RectOffset(0, 0, 4, 4);
            horizontalLine.fixedHeight = 1;

            var c = GUI.color;
            GUI.color = color;
            GUILayout.Box(GUIContent.none, horizontalLine);
            GUI.color = c;
        }
    }
#endif
}