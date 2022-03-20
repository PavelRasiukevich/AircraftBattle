using UnityEditor;
using UnityEditor.UI;
#if UNITY_EDITOR
using Enums;
#endif

namespace UI.Buttons.WeaponButton
{
#if UNITY_EDITOR

    [CustomEditor(typeof(WeaponButton))]
    public class WeaponButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            WeaponButton targetWeaponButton = (WeaponButton) target;
            targetWeaponButton.Type = (BulletType) EditorGUILayout.EnumPopup("BulletType: ", targetWeaponButton.Type);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}