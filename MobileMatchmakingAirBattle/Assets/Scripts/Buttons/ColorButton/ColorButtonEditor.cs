#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

namespace Buttons.ColorButton
{
#if UNITY_EDITOR

    [CustomEditor(typeof(ColorButton))]
    public class ColorButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            ColorButton targetColorButton = (ColorButton)target;

            targetColorButton.Color = EditorGUILayout.ColorField("Color: ", targetColorButton.Color);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_event"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}