#if UNITY_EDITOR
using UnityEditor.UI;
using UnityEditor;
#endif

namespace UI.Buttons.ColorButton
{
#if UNITY_EDITOR

    [CustomEditor(typeof(ColorButton))]
    public class ColorButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            ColorButton targetColorButton = (ColorButton) target;
            targetColorButton.Color = EditorGUILayout.ColorField("Color: ", targetColorButton.Color);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}