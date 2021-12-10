using System.Collections.Generic;
using Assets.Scripts.AirCrafts;
using ScriptableObjects;
using TO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace CustomEditors
{
#if UNITY_EDITOR

    public class PlanesDataEditor : BaseCustomEditor
    {
        private PlanesDataScriptableObject _planesData;

        private ReorderableList reorderableList = null;

        private PlaneInfo _selectedPlane;

        [MenuItem("Window/Game Data/Planes")]
        static void Init() => GetWindow(typeof(PlanesDataEditor));

        private void OnEnable()
        {
            _planesData =
                AssetDatabase.LoadAssetAtPath("Assets/PlanesData.asset",
                    typeof(PlanesDataScriptableObject)) as PlanesDataScriptableObject;
            reorderableList = new ReorderableList(_planesData._planeList, typeof(PlaneInfo));
            reorderableList.elementHeight =
                EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            reorderableList.onSelectCallback = list => _selectedPlane = _planesData._planeList[list.index];
            reorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Planes:", EditorStyles.boldLabel);
            reorderableList.drawElementCallback =
                (rect, index, isactive, isfocused) =>
                    EditorGUI.LabelField(rect,
                        _planesData._planeList[index]._displayName,
                        _planesData._planeList[index]._isDefaultPlane ? EditorStyles.boldLabel : EditorStyles.label);
        }

        private void DrawSelected()
        {
            if (_selectedPlane == null) return;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Plane: {_selectedPlane._displayName}", EditorStyles.boldLabel);

            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginDisabledGroup(true);
            if (_selectedPlane._id == 0) _selectedPlane._id = Random.Range(100000000, 999999999);
            EditorGUILayout.IntField("ID", _selectedPlane._id);
            EditorGUI.EndDisabledGroup();
            _selectedPlane._displayName = EditorGUILayout.TextField("Name", _selectedPlane._displayName);
            _selectedPlane._isViewInEditor =
                EditorGUILayout.Foldout(_selectedPlane._isViewInEditor, $"Plane: {_selectedPlane._displayName}");

            if (_selectedPlane._isViewInEditor)
            {
                _selectedPlane._icon = (Sprite) EditorGUILayout.ObjectField("Icon", _selectedPlane._icon,
                    typeof(Sprite),
                    allowSceneObjects: true);
                _selectedPlane._planePrefab = (AirCraft) EditorGUILayout.ObjectField("Prefab",
                    _selectedPlane._planePrefab,
                    typeof(AirCraft), allowSceneObjects: true);
                _selectedPlane._isDefaultPlane =
                    EditorGUILayout.Toggle("Default Plane", _selectedPlane._isDefaultPlane);
                EditorGUI.BeginDisabledGroup(_selectedPlane._isDefaultPlane);
                _selectedPlane._gamePrice =
                    EditorGUILayout.FloatField("Game price",
                        _selectedPlane._isDefaultPlane ? 0 : _selectedPlane._gamePrice);
                EditorGUI.EndDisabledGroup();
            }
        }

        private void OnGUI()
        {
            List<PlaneInfo> planesInEditor = new List<PlaneInfo>();
            reorderableList.DoLayoutList();
            DrawSelected();

            if (_planesData._planeList.Count > 0)
                planesInEditor.AddRange(_planesData._planeList);

            _planesData._planeList.Clear();
            _planesData._planeList.AddRange(planesInEditor);
            EditorGUI.indentLevel = 0;
        }
    }
#endif
}